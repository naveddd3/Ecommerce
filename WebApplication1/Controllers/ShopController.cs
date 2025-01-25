using Domain.Entities;
using Domain.Enum;
using Domain.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using WEBAPP.Models.Helper;

namespace WEBAPP.Controllers
{
    [Authorize(Roles = MasterRole.ADMIN)]
    public class ShopController : Controller
    {
        private readonly string _BaseUrl;
        public ShopController(AppSetting appSetting)
        {
            _BaseUrl = appSetting.WebApiBaseUrl;
        }
        [HttpGet("OnboardingVendors")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PendingShopsPartial()
        {
            var list = new Response<List<Shop>>();
            var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Shop/GetPendingShops", null, User.GetLoggedInUserToken());
            if (apiRes != null)
            {
                list = JsonConvert.DeserializeObject<Response<List<Shop>>>(apiRes.Result);
            }
            return PartialView(list.Result);
        }

        [HttpGet(nameof(Vendors))]
        public IActionResult Vendors()
        {
            return View();
        }

        public async Task<IActionResult> VendorsPartial()
        {
            var list = new Response<List<Shop>>();
            var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Shop/GetShops", null, User.GetLoggedInUserToken());
            if (apiRes != null)
            {
                list = JsonConvert.DeserializeObject<Response<List<Shop>>>(apiRes.Result);
            }
            return PartialView(list.Result);
        }

        public async Task<IActionResult> UpdateShopVStatus(ShopVerification shopVerification)
        {
            var res = new Response();
            var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Shop/UpdateVerificationStatus", JsonConvert.SerializeObject(shopVerification), User.GetLoggedInUserToken());
            if (apiRes != null)
            {
                 res  = JsonConvert.DeserializeObject<Response>(apiRes.Result);
            }
            return Json(res);
        }

    }
}
