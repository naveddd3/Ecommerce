using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        [HttpPost(nameof(ReverseGeocode))]
        public async Task<IActionResult> ReverseGeocode([FromQuery] double latitude, [FromQuery] double longitude)
        {
            var client = new HttpClient();
            var apiUrl = $"http://farazapi.runasp.net/api/Map/ReverseGeocodeAsync?latitude={latitude}&longitude={longitude}&SecretKey=zaAgkwufYHSNHOXDMvskXk1MpkTNt2RA";

            try
            {
                var response = await client.GetStringAsync(apiUrl);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message);
            }
        }
    }
}
