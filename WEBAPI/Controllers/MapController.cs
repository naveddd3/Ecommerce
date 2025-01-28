using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
		private readonly IMapService _mapService;

		public MapController(IMapService mapService)
		{
			_mapService = mapService;
		}

		[HttpPost(nameof(GetUserLocation))]
        public async Task<IActionResult> GetUserLocation([FromQuery] double latitude, [FromQuery] double longitude)
        {
            var res = await _mapService.GetUserLocationAndDeliveryTime(latitude, longitude);
            return Ok(res);
        }
    }
}
