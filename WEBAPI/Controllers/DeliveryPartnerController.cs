using Application.Interfaces;
using Domain.Entities;
using Infrastucture.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEBAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DeliveryPartnerController : ControllerBase
	{
		private readonly IDeliveryPartnersServices _deliverypartnersServices;
        private readonly FileUploadService _fileUploadService;

        public DeliveryPartnerController(IDeliveryPartnersServices deliverypartnersServices, FileUploadService fileUploadService)
		{
			_deliverypartnersServices = deliverypartnersServices;
			_fileUploadService = fileUploadService;
		}
        [HttpPost(nameof(GetDeliveryPartner))]
        public async Task<IActionResult> GetDeliveryPartner()
        {
            var res = await _deliverypartnersServices.GetAllDeliveryPartners();
            return Ok(res);
        }

        [HttpGet(nameof(GetDeliveryPartnerById))]
		public async Task<IActionResult> GetDeliveryPartnerById(int ID)
		{
			var res = await _deliverypartnersServices.GetDeliveryPartnerById(ID);
			return Ok(res);
		}
		[HttpPost(nameof(RegisterDeliveryPartner))]
		public async Task<IActionResult> RegisterDeliveryPartner([FromForm]DeliveryPartner deliveryPartner)
		{
            if (deliveryPartner.ProfilePicture != null)
            {
                var files = _fileUploadService.Image(deliveryPartner.ProfilePicture, FileUploadPath.ProductImage);
                deliveryPartner.ImageUrl = files;
            }
            var res = await _deliverypartnersServices.RegisterDeliveryPartner(deliveryPartner);
			return Ok(res);
		}
	}
}
