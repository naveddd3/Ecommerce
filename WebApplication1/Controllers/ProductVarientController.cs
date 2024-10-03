using Domain.Entities;
using Domain.Helper;
using Infrastucture.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEBAPP.Models.Helper;

namespace WEBAPP.Controllers
{
    public class ProductVarientController : Controller
    {
        private readonly string _BaseUrl;
        private readonly FileUploadService _fileUploadService;
        public ProductVarientController(AppSetting appSetting, FileUploadService fileUpload)
        {
            _BaseUrl=appSetting.WebApiBaseUrl;
            _fileUploadService = fileUpload;
        }
        [Route("ProductVarient")]
        public IActionResult Index()
        {
            return View();
        }

        #region ForWebsite
        [Route("p/{CategoryName?}/{CategoryId?}")]
        public async Task<IActionResult> SubCategoryOnCategory( int CategoryId)
        {
            var model = new WebsiteModel();
            var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/ProductVarient/SubCategoryOnCategory/{CategoryId}", null, null);
            if (apiRes != null)
            {
                model.SubCategories = JsonConvert.DeserializeObject<List<SubCategory>>(apiRes.Result);
            }
            return View(model);
        }

        public async Task<IActionResult> ProductOnSubCategory(int SubCategoryId)
        {
            var model = new WebsiteModel();
            var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/ProductVarient/ProductOnSubCategoryId/{SubCategoryId}", null, null);
            if (apiRes != null)
            {
                model.ProductVarientAPIRES = JsonConvert.DeserializeObject<ProductVarientAPIRES>(apiRes.Result);
            }
            return PartialView(model);
        }

        public async Task<IActionResult> ProductDetail(int ProductId)
            {
            var model = new WebsiteModel();
            var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/ProductVarient/ProductDetailById/{ProductId}", null,null);
            if(apiRes != null)
            {
                model.ProductDetail = JsonConvert.DeserializeObject<ProductDetail>(apiRes.Result);
            }
            return PartialView(model);

        }
        #endregion
    }
}
