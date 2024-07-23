﻿using Azure;
using Domain.Entities;
using Domain.Helper;
using Infrastucture.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using WEBAPP.Models.Helper;
using Response = Domain.Entities.Response;

namespace WEBAPP.Controllers
{
    public class ProductController : Controller
    {
        private readonly string _BaseUrl;
        private readonly FileUploadService _fileUploadService;
        public ProductController(AppSetting appSetting, FileUploadService fileUpload)
        {
            _BaseUrl=appSetting.WebApiBaseUrl;
            _fileUploadService = fileUpload;
        }

        [Route("Products")]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetProducts()
        {
            var list = new ApiResponseProduct();
            try
            {
                var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Product/GetProduct", null, User.GetLoggedInUserToken());
                if (apiRes != null)
                {
                  list = JsonConvert.DeserializeObject<ApiResponseProduct>(apiRes.Result);
                    
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return PartialView(list);
        }
        public async Task<IActionResult> AddOrEditProduct(int Id)
        {
            ProductVM category = new ProductVM();
            try
            {
                var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Product/GetProductById/{Id}", null, User.GetLoggedInUserToken());
                if (apiRes.Result != null)
                {
                    category = JsonConvert.DeserializeObject<ProductVM>(apiRes.Result);
                }
                return PartialView(category);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateProduct([FromForm] string Jsondata, [FromForm] IEnumerable<IFormFile> productImage)
        {
            var res = new Response()
            {
                ResponseText="An error Has Been Occured !",
                StatusCode = ResponseStatus.Failed
            };
            try
            {
                var request = JsonConvert.DeserializeObject<ProductReq>(Jsondata);
                request.Images = productImage;
                if (request.ProductId == 0)
                {
                    if (!productImage.Any())
                    {
                        res.ResponseText = "Please Upload Images!";
                        return Json(res);
                    }
                }
                var apiRes = await AppWebRequest.O.SendFileAndContentAsync($"{_BaseUrl}/api/Product/SaveProduct",  User.GetLoggedInUserToken(), request);
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

        public async Task<IActionResult> ShowImagesOfProduct(int ID)
        {
            var list = new List<ProductImage>();
            var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Product/ShowImagesOfProduct/{ID}", null, User.GetLoggedInUserToken());
            if (apiRes != null)
            {
                list = JsonConvert.DeserializeObject<List<ProductImage>>(apiRes.Result);
            }
            return Json(list);
        }

        public async Task<IActionResult> DeleteImageOfProduct(int ID)
        {
            var res = new Response()
            {
                ResponseText="An Error Has Been Occured !",
                StatusCode = ResponseStatus.Failed
            };
            var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Product/DeleteImageOfProduct/{ID}", null, User.GetLoggedInUserToken());
            if (apiRes != null)
            {
                res = JsonConvert.DeserializeObject<Response>(apiRes.Result);
            }
            return Json(res);
        }
    }
}
