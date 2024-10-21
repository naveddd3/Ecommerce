using Application.Interfaces;
using Domain.Entities;
using Domain.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedAddressController : ControllerBase
    {
        private readonly ISavedAddressService _savedAddress;

        public SavedAddressController(ISavedAddressService savedAddress)
        {
            _savedAddress = savedAddress;
        }

        [HttpPost(nameof(SaveAddress))]
        public async Task<IActionResult> SaveAddress(SavedAddress savedAddress)
        {
            return Ok(await _savedAddress.SaveAddress(savedAddress));
        }

        [HttpPost(nameof(GetAddressByUserId))]
        public async Task<IActionResult> GetAddressByUserId(int UserId)
        {
            UserId = User.GetLoggedInUserId<int>();
            var res = await _savedAddress.GetAddressByUserId(UserId);
            return Ok(res);
        }
    }
}
