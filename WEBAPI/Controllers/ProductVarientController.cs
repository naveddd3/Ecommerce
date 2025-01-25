using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductVarientController : ControllerBase
    {
        private readonly IProductVarientService _productVarientService;

        public ProductVarientController(IProductVarientService productVarientService)
        {
            _productVarientService=productVarientService;
        }

        [HttpPost(nameof(SubCategoryOnCategory) +"/{CategoryId}")]
        public async Task<IActionResult> SubCategoryOnCategory(int CategoryId)
        {
            var res = await _productVarientService.SubCategoryOnCategory(CategoryId);
            return Ok(res);
        }

        [HttpPost(nameof(ProductOnSubCategoryId) + "/{SubCategoryId}"+ "/{UserLattitude}" + "/{UserLongitude}")]
        public async Task<IActionResult> ProductOnSubCategoryId(int SubCategoryId, decimal UserLattitude, decimal UserLongitude)
        {
            var res = await _productVarientService.ProductOnSubCategoryId(SubCategoryId, UserLattitude, UserLongitude);
            return Ok(res);
        }

        [HttpPost(nameof(ProductDetailById) + "/{ProductId}")]
        public async Task<IActionResult> ProductDetailById(int ProductId)
        {
            var res = await _productVarientService.ProductDetailById(ProductId);
            return Ok(res); 
        }

        [HttpPost(nameof(PlaceOrder))]
        public async Task<IActionResult> PlaceOrder(int ProductId)
        {
            var res = await _productVarientService.ProductDetailById(ProductId);
            return Ok(res);
        }
    }
}
