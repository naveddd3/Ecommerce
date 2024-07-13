using Application.Interfaces;
using Domain.Entities;
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
            _productService=productService;
            _fileUploadService=fileUpload;
        }
        [HttpPost(nameof(SaveProduct))]
        public async Task<IActionResult> SaveProduct([FromForm] ProductReq product)
        {
            var res = new Response() { ResponseText=string.Empty, StatusCode= ResponseStatus.Failed };
            if (product.ProductId==0)
            {
                if (!product.Images.Any())
                {
                    res.ResponseText = "Please Upload Images";
                    res.StatusCode = ResponseStatus.Failed;
                    return Ok(res);
                }
            }
            List<string> imgPath = new List<string>();
            if (product.Images!=null)
            {
                foreach (var images in product.Images)
                {
                    var files = _fileUploadService.UploadImage(images, FileUploadPath.ProductImage);
                    res.StatusCode = files.StatusCode;
                    imgPath.Add(files.ResponseText);
                }

            }
            product.ImageUrl = string.Join(",", imgPath);
            var response = await _productService.SaveOrUpdateProduct(product);
            return Ok(response);
        }
        [HttpPost(nameof(GetProduct))]
        public async Task<IActionResult> GetProduct()
        {
            var res = await _productService.GetProduct();
            return Ok(res);
        }

        [HttpPost(nameof(GetProductById)+"/{Id}")]
        public async Task<IActionResult> GetProductById(int Id)
        {
            var res = await _productService.GetProductById(Id);
            return Ok(res);
        }
    }
}
