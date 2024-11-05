using Application.Interfaces;
using Domain.Entities;
using Infrastucture.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly FileUploadService _fileUploadService;
        private readonly IOTPService _oTPService;
        public AccountController(IUserService userService, FileUploadService fileUpload, IOTPService oTPService)
        {
            _userService = userService;
            _fileUploadService = fileUpload;
            _oTPService = oTPService;
        }
        [HttpPost(nameof(SignUp))]
        public async Task<IActionResult> SignUp(SignUpReq signUpReq)
        {
            var res = await _userService.SignUp(signUpReq);
            return Ok(res);
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginReq loginReq)
        {
            var res = await _userService.Login(loginReq);
            return Ok(res);
        }

        [HttpPost(nameof(SaveShop))]
        public async Task<IActionResult> SaveShop([FromForm] ShopReq shopReq)
        {
            var res = new Response()
            {
                ResponseText = "Something Went Wrong",
                StatusCode = ResponseStatus.Failed
            };
            if (shopReq.BusinessLicense == null)
            {
                res.ResponseText = "Please Upload Business License";
                res.StatusCode = ResponseStatus.Failed;
                return Ok(res);
            }
            List<string> Images = new List<string>();
            if (shopReq.BusinessLicense != null)
            {
                foreach (var i in shopReq.BusinessLicense)
                {
                    var file = _fileUploadService.UploadImage(i, FileUploadPath.BusinessLicense);
                    res.StatusCode = file.StatusCode;
                    Images.Add(file.ResponseText);
                }
            }
            shopReq.BusinessLicensePath = string.Join(",", Images);
            res = await _userService.SaveShopOwner(shopReq);
            return Ok(res);
        }

        [HttpPost(nameof(VerifyEmail))]
        public async Task<IActionResult> VerifyEmail(VerifyEmail email)
        {
            if (email.OTP == 0 || email.OTP == null)
            {
                var res = await _oTPService.SendOTP(new OTPRequest
                {
                    Email = email.Email,
                    OTP = email.OTP,
                    Method = "VRFYEML"
                });
                return Ok(res);
            }
            else
            {
                var res = await _oTPService.MatchOTP(new OTPRequest
                {
                    Email = email.Email,
                    OTP = email.OTP,
                    Method = "VRFYEML"
                });
                if (res.StatusCode == ResponseStatus.Success)
                {
                    res.ResponseText = "Verified Email";
                }
                return Ok(res);
            }
        }

        [HttpPost(nameof(LoginviaOTP))]
        public async Task<IActionResult> LoginviaOTP(LoginviaOTPReq loginviaOTPReq)
        {
            var response = new Response()
            {
                ResponseText = "An Error has been Occured",
                StatusCode = ResponseStatus.Failed
            };
            if (loginviaOTPReq.OTP == 0 || loginviaOTPReq.OTP == null)
            {
                response = await _oTPService.SendOTP(new OTPRequest
                {
                    Email = loginviaOTPReq.EmailOrMobile,
                    OTP = loginviaOTPReq.OTP,
                    Method = "LGNREQ"
                });
                return Ok(response);
            }
            else
            {
                if (loginviaOTPReq.OTP!=null || loginviaOTPReq.OTP != 0)
                {
                    var IsValid = await _oTPService.MatchOTP(new  OTPRequest
                    {
                        Email = loginviaOTPReq.EmailOrMobile,
                        OTP = loginviaOTPReq.OTP,
                        Method = "LGNREQ"
                    });
                    if (IsValid.StatusCode == ResponseStatus.Success)
                    {
                        var res = await _userService.LoginviaOTP(loginviaOTPReq);
                        return Ok(res);
                    }

                }

            }
            return BadRequest(response);

        }

    }
}
