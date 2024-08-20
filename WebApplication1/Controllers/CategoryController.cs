using Azure;
using Domain.Entities;
using Domain.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEBAPP.Models.Helper;

namespace WEBAPP.Controllers
{
    public class CategoryController : Controller
    {
        private readonly string _BaseUrl;
        public CategoryController(AppSetting baseUrl)
        {
            _BaseUrl=baseUrl.WebApiBaseUrl;
        }

        [Route("Category")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AllCategory()
        {
            var list = new List<MasterCategory>();
            try
            {
                var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Category/GetCategory", null, User.GetLoggedInUserToken());
                if (apiRes != null)
                {
                    list = JsonConvert.DeserializeObject<List<MasterCategory>>(apiRes.Result);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return PartialView(list);
        }
        public async Task<IActionResult> AddOrEditCategory(int Id)
        {
            MasterCategory category = new MasterCategory();
            try
            {
                var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Category/GetCategoryById/{Id}", null, User.GetLoggedInUserToken());
                if (apiRes.Result != null)
                {
                    category = JsonConvert.DeserializeObject<MasterCategory>(apiRes.Result);    
                }
                return PartialView(category);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IActionResult> SaveOrUpdateCategory([FromForm] string Jsondata, [FromForm] IFormFile CategoryImage)
        {
            var res = new Domain.Entities.Response()
            {
                ResponseText="An error Has Been Occured !",
                StatusCode = ResponseStatus.Failed
            };
            var request = JsonConvert.DeserializeObject<MasterCategory>(Jsondata);
            request.ImagePath = CategoryImage;
            try
            {
                var apiRes = await AppWebRequest.O.SendFileAndContentAsync($"{_BaseUrl}/api/Category/SaveCategory",User.GetLoggedInUserToken(),request);
                var response = await apiRes.Content.ReadAsStringAsync();
                if (apiRes != null)
                {
                    res = JsonConvert.DeserializeObject<Domain.Entities.Response>(response);
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
