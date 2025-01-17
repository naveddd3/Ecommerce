using Domain.Entities;
using Domain.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEBAPP.Models.Helper;

namespace WEBAPP.Controllers
{
	public class DeliveryPartnerController : Controller
	{
        private readonly string _BaseUrl;
        public DeliveryPartnerController(AppSetting appSetting)
        {
            _BaseUrl = appSetting.WebApiBaseUrl;
        }
        [HttpGet("DeliveryPartner")]
		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> PartialDeliveryPartnerList()
		{
            var list = new List<DeliveryPartner>();
            try
            {
                var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/DeliveryPartner/GetDeliveryPartner", null, User.GetLoggedInUserToken());
                if (apiRes != null)
                {
                    list = JsonConvert.DeserializeObject<List<DeliveryPartner>>(apiRes.Result);

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return PartialView(list);
        }

        public async Task<IActionResult> AddOrEditDeliveryPartner(int Id)
        {
            var deliveryPartner = new DeliveryPartner();
            try
            {
                var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/DeliveryPartner/GetDeliveryPartnerById/{Id}", null, User.GetLoggedInUserToken());
                if (apiRes.Result != null)
                {
                    deliveryPartner = JsonConvert.DeserializeObject<DeliveryPartner>(apiRes.Result);
                }
                return PartialView(deliveryPartner);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateDeliveryPartner([FromForm] string Jsondata, [FromForm] IFormFile ProfilePicture)
        {
            var res = new Response()
            {
                ResponseText = "An error Has Been Occured !",
                StatusCode = ResponseStatus.Failed
            };
            try
            {
                var request = JsonConvert.DeserializeObject<DeliveryPartner>(Jsondata);
                request.ProfilePicture = ProfilePicture;
                if (request.ProfilePicture == null)
                {
                    if (ProfilePicture == null)
                    {
                        res.ResponseText = "Please Upload Profile Picture!";
                        return Json(res);
                    }
                }
                var apiRes = await AppWebRequest.O.SendFileAndContentAsync($"{_BaseUrl}/api/DeliveryPartner/RegisterDeliveryPartner", User.GetLoggedInUserToken(), request);
                var response = await apiRes.Content.ReadAsStringAsync();
                if (apiRes != null)
                {
                    res = JsonConvert.DeserializeObject<Response>(response);
                }
                return Json(res);
            }

            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
