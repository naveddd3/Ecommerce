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
using Microsoft.AspNetCore.Components.Forms;

namespace WEBAPP.Controllers
{
    public class AccountController : Controller
    {
        private string _BaseUrl;
        public AccountController(AppSetting appSetting)
        {
            _BaseUrl = appSetting.WebApiBaseUrl;
        }

        [Route("AdminLogin")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DoLogin(LoginReq loginReq)
        {
            var res = new Response()
            {
                StatusCode = ResponseStatus.Failed,
                ResponseText = "Unable To Login !"
            };
            try
            {
                if (ModelState.IsValid)
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

        [Route("Login")]
        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }

        [Route("RegisterShop")]
        public IActionResult RegisterShop()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterYourShop()
        {
            return PartialView();
        }

        public async Task<IActionResult> SaveShop([FromForm] string Jsondata, [FromForm] IEnumerable<IFormFile> BusinessLicense)
        {
            var res = new Response()
            {
                ResponseText ="Something Went Wrong !",
                StatusCode = ResponseStatus.Failed,
            };
            var request = JsonConvert.DeserializeObject<ShopReq>(Jsondata);
            request.BusinessLicense = BusinessLicense;
            var apiRes = await AppWebRequest.O.SendFileAndContentAsync($"{_BaseUrl}/api/Account/SaveShop", User.GetLoggedInUserToken(), request);
            var response = await apiRes.Content.ReadAsStringAsync();
            if (apiRes != null)
            {
                res = JsonConvert.DeserializeObject<Response>(response);
            }
            return Json(res);
        }


        [HttpPost]
        public async Task<IActionResult> SendOTP(string email)
        {
            var res = new Response() { };
            var ve = new VerifyEmail();
            ve.Email = email;
            ve.OTP = 0;
            var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Account/VerifyEmail", JsonConvert.SerializeObject(ve), User.GetLoggedInUserRole());
            if(apiRes != null)
            {
                res = JsonConvert.DeserializeObject<Response>(apiRes.Result);
            }
            return Json(res);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmail(int OTP , string email)
        {
            var res = new Response() { };
            var ve = new VerifyEmail();
            ve.Email = email;
            ve.OTP = OTP;
            var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Account/VerifyEmail", JsonConvert.SerializeObject(ve), User.GetLoggedInUserRole());
            if (apiRes != null)
            {
                res = JsonConvert.DeserializeObject<Response>(apiRes.Result);
            }
            return Json(res);
        }


    }
}
