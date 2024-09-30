using Application.Interfaces;
using Domain.Entities;
using Domain.Helper;
using Infrastucture.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly FileUploadService _fileUploadService;
        public ProductController(IProductService productService, FileUploadService fileUpload)
        {
            _productService = productService;
            _fileUploadService = fileUpload;
        }
        #region ForAdmin
        [HttpPost(nameof(SaveProduct))]
        public async Task<IActionResult> SaveProduct([FromForm] ProductReq product)
        {
            var res = new Response() { ResponseText = string.Empty, StatusCode = ResponseStatus.Failed };
            if (product.ProductId == 0)
            {
                if (product.ProductImage == null)
                {
                    res.ResponseText = "Please Upload Images";
                    res.StatusCode = ResponseStatus.Failed;
                    return Ok(res);
                }
            }
            if (product.ProductImage != null)
            {
                var files = _fileUploadService.Image(product.ProductImage, FileUploadPath.ProductImage);
                res.StatusCode = ResponseStatus.Success;
                product.ImageUrl = files;
            }
            var response = await _productService.SaveOrUpdateProduct(product);
            return Ok(response);
        }
        [HttpPost(nameof(GetProduct))]
        public async Task<IActionResult> GetProduct()
        {
            var res = await _productService.GetProduct();
            return Ok(res);
        }

        [HttpPost(nameof(GetProductById) + "/{Id}")]
        public async Task<IActionResult> GetProductById(int Id)
        {
            var res = await _productService.GetProductById(Id);
            return Ok(res);
        }

        [HttpPost(nameof(ShowImagesOfProduct) + "/{ID}")]
        public async Task<IActionResult> ShowImagesOfProduct(int ID)
        {
            var res = await _productService.ShowImagesOfProduct(ID);
            return Ok(res);
        }

        [HttpPost(nameof(DeleteImageOfProduct) + "/{Id}")]
        public async Task<IActionResult> DeleteImageOfProduct(int Id)
        {
            var res = await _productService.DeleteImageOfProduct(Id);
            return Ok(res);
        }

        [HttpPost(nameof(SaveProductVarient))]
        public async Task<IActionResult> SaveProductVarient([FromForm] ProductVarientReq productVarient)
        {
            var res = new Response() { ResponseText = string.Empty, StatusCode = ResponseStatus.Failed };
            if (productVarient.VarientId == 0)
            {
                if (!productVarient.productvarientImages.Any())
                {
                    res.ResponseText = "Please Upload Images";
                    res.StatusCode = ResponseStatus.Failed;
                    return Ok(res);
                }
            }
            List<string> imgPath = new List<string>();
            if (productVarient.productvarientImages != null)
            {
                foreach (var images in productVarient.productvarientImages)
                {
                    var files = _fileUploadService.Image(images, FileUploadPath.ProductVarient);
                    imgPath.Add(files);
                }

            }
            productVarient.ImageUrl = string.Join(",", imgPath);
            var response = await _productService.SaveProductVarient(productVarient);
            return Ok(response);
        }

        [HttpPost(nameof(GetProductVarient) + "/{Id}")]
        public async Task<IActionResult> GetProductVarient(int Id)
        {
            var res = await _productService.GetProductVarient(Id);
            return Ok(res);
        }

        [HttpPost(nameof(GetProductVarientImage) + "/{Id}")]
        public async Task<IActionResult> GetProductVarientImage(int Id)
        {
            var res = await _productService.GetProductVarientImage(Id);
            return Ok(res);
        }

        [HttpPost(nameof(GetProductVarientById) + "/{ProductId}"+ "/{VarientId}")]
        public async Task<IActionResult> GetProductVarientById(int ProductId,int VarientId)
        {
            var res = await _productService.GetProductVarientById(ProductId, VarientId);
            return Ok(res);
        }

        [HttpPost(nameof(SaveOrUpdateSliderImage))]
        public async Task<IActionResult> SaveOrUpdateSliderImage(ProductSliderImages productSliderImages)
        {
            var res = new Response()
            {
                ResponseText = "An Error Has Been Occured !",
                StatusCode = ResponseStatus.Failed
            };
            if (productSliderImages.ProductSliderImageId == 0 || productSliderImages.ProductSliderImageId == null)
            {
                var sliderImages = await _productService.GetProductSliderImagesById(productSliderImages.ProductId);
                if (sliderImages.Count() >= 4)
                {
                    res.ResponseText = "Can Not Insert More than 4 Slider Images !";
                    res.StatusCode = ResponseStatus.Failed;
                    return Ok(res);
                }
            }
            List<string> images = new List<string>();
            foreach (var image in productSliderImages.SliderImages)
            {
                string file = _fileUploadService.Image(image, FileUploadPath.ProductSliderImages);
                images.Add(file);
            }
            productSliderImages.Images = string.Join(",", images);
            res = await _productService.SaveOrUpdateProductSlider(productSliderImages);
            return Ok(res);
        }

        [HttpPost(nameof(GetSliderImagesById)+"/{Id}")]
        public async Task<IActionResult> GetSliderImagesById(int Id)
        {
            var res =  await _productService.GetProductSliderImagesById(Id);
            return Ok(res); 
        }
        #endregion


    }
}
