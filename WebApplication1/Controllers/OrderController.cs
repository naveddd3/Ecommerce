using Domain.Entities;
using Domain.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEBAPP.Models.Helper;

namespace WEBAPP.Controllers
{
    [Authorize(Roles = "Admin,Vendor")]
    public class OrderController : Controller
    {
        private readonly string _BaseUrl;

        public OrderController(AppSetting baseUrl)
        {
            _BaseUrl = baseUrl.WebApiBaseUrl;
        }

        [Route("Orders")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetOrders()
        {
            var list = new List<OrderDetails>();
            var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Order/GetOrders", null, User.GetLoggedInUserToken());
            if(apiRes != null)
            {
                list = JsonConvert.DeserializeObject<List<OrderDetails>>(apiRes.Result);
            }
            return PartialView(list);
        } 
    }
}
