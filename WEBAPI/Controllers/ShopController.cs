using Application.Interfaces;
using Domain.Entities;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEBAPI.Controllers
{
	[Authorize(Roles = MasterRole.ADMIN)]
	[Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpPost(nameof(GetShops))]
        public async Task<IActionResult> GetShops()
        {
            var res = await _shopService.GetShops();
            return Ok(res);
        }
        [HttpPost(nameof(GetPendingShops))]
        public async Task<IActionResult> GetPendingShops()
        {
            var res = await _shopService.GetPendingShops();
            return Ok(res);
        }
        [HttpPost(nameof(UpdateVerificationStatus))]
        public async Task<IActionResult> UpdateVerificationStatus([FromBody] ShopVerification shopVerification)
        {
            var res = await _shopService.UpdateVerificationStatus(shopVerification);
            return Ok(res);
        }
    }
}
