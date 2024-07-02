using Application.Interfaces;
using Domain.Entities;
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
        public AccountController(IUserService userService)
        {
                _userService = userService;
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

        
    }
}
