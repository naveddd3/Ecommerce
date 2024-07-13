using Domain.Entities;
using Domain.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Net;
using WEBAPP.Models.Helper;

namespace WEBAPP.Controllers
{
    public class AccountController : Controller
    {
        private string _BaseUrl;
        public AccountController(AppSetting appSetting)
        {
            _BaseUrl = appSetting.WebApiBaseUrl;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginReq loginReq)
        {
            var res = new Response()
            {
                StatusCode = ResponseStatus.Failed,
                ResponseText = "Unable To Login !"
            };
            try
            {
               if(ModelState.IsValid)
                {
                    var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Account/Login", JsonConvert.SerializeObject(loginReq), null);
                    if (apiRes != null)
                    {
                        var authenticateResponse = JsonConvert.DeserializeObject<Response<LoginResponse>>(apiRes.Result);
                        if (authenticateResponse.StatusCode != ResponseStatus.Success)
                        {
                            res.ResponseText = authenticateResponse.ResponseText;
                            return BadRequest(res);
                        }
                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                        identity.AddClaim(new Claim("Token", authenticateResponse.Result.Token));
                        identity.AddClaim(new Claim(ClaimTypes.Role, authenticateResponse.Result.Role));
                        identity.AddClaim(new Claim(ClaimTypes.Name, authenticateResponse.Result.Name));
                        identity.AddClaim(new Claim("UserId", authenticateResponse.Result.UserId.ToString()));
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                        string redirectUrl = $"/{authenticateResponse.Result.Role}";
                        return Redirect(redirectUrl);
                    }
                }
                return BadRequest(res);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpReq signUpReq)
        {
            signUpReq.Role = "User";
            var res = new Response();
            var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Account/SignUp", JsonConvert.SerializeObject(signUpReq));
            var authenticateResponse = JsonConvert.DeserializeObject<Response>(apiRes.Result);

            if (authenticateResponse.StatusCode != ResponseStatus.Success)
            {
                res.ResponseText = authenticateResponse.ResponseText;
                return Json(authenticateResponse);
            }
            return Redirect("/Account/Login");
        }
        public async Task<IActionResult> Logout(string returnUrl = "/Account/Login")
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            HttpContext.Response.Cookies.Delete(".AspNetCore.Identity.Application");
            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public  IActionResult UserLogin()
        {
            return View();
        }
    }
}
