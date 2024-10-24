﻿using Domain.Entities;
using Domain.Helper;
using Infrastucture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEBAPP.Models.Helper;

namespace WEBAPP.Controllers
{
    public class WebsiteController : Controller
    {
        private readonly string _BaseUrl;
        private readonly FileUploadService _fileUploadService;
        public WebsiteController(AppSetting appSetting, FileUploadService fileUpload)
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
        public async Task<IActionResult> GetProductVarientById(int ProductId)
        {
            var list = new List<ProductVarientRes>();
            var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Product/GetProductVarient/{ProductId}", null, null);
            if (apiRes != null)
            {
                list = JsonConvert.DeserializeObject<List<ProductVarientRes>>(apiRes.Result);
            }
            return PartialView(list);

        }

        [Route("Checkout")]
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var addrslist = new List<SavedAddress>();
            var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/SavedAddress/GetAddressByUserId", null, User.GetLoggedInUserToken());
            if (apiRes != null)
            {
                addrslist = JsonConvert.DeserializeObject<List<SavedAddress>>(apiRes.Result);
            }
            return PartialView(addrslist);
        }

        public async Task<IActionResult> PlaceOrder(CheckoutDetails cart)
        {
            return Ok(cart);
        }

        public async Task<IActionResult> CreateNewAddress()
        {
            return PartialView();
        }
        #endregion
    }
}
