using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService=categoryService;
        }

        [HttpPost(nameof(SaveCategory))]
        public async Task<IActionResult> SaveCategory(MasterCategory category)
        {
            var res = await _categoryService.SaveOrUpdateCategory(category);
            return Ok(res); 
        }

        [HttpPost(nameof(GetCategory))]
        public async Task<IActionResult> GetCategory()
        {
            var res = await _categoryService.GetCategory();
            return Ok(res);
        }

        [HttpPost(nameof(GetCategoryById))]
        public async Task<IActionResult> GetCategoryById(int Id)
        {
            var res = await _categoryService.AddOrEditCategory(Id);
            return Ok(res);
        }
    }
}
