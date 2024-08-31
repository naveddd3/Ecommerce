﻿using Application.Interfaces;
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

        [HttpPost(nameof(VarientOnProductId)+"/{ProductId}")]
        public async Task<IActionResult> VarientOnProductId(int ProductId)
        {
            var res = await _productVarientService.VarientOnProduct(ProductId);
            return Ok(res);
        }
    }
}
