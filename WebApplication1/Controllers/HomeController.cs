using Domain.Entities;
using Domain.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEBAPP.Models.Helper;

namespace WEBAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _BaseUrl;

        public HomeController(AppSetting baseUrl)
        {
            _BaseUrl=baseUrl.WebApiBaseUrl;
        }

        public async Task<IActionResult> Index()
        {
            var categoryList = await GetCategory();
            return View(categoryList);
        }

        private async Task<WebsiteModel> GetCategory()
        {
            var category = new WebsiteModel();
            var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Category/GetCategory", null, null);
            if (apiRes != null)
            {
                 category.Categories = JsonConvert.DeserializeObject<List<MasterCategory>>(apiRes.Result);
            }
            return category;

        }
    }
}
