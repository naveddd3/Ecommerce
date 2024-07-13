using Application.Interfaces;
using Domain.Entities;
using Infrastucture.Services;
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
        private readonly FileUploadService _fileUploadService;
        public CategoryController(ICategoryService categoryService , FileUploadService fileUploadService)
        {
            _categoryService=categoryService;
            _fileUploadService=fileUploadService;
        }

        [HttpPost(nameof(SaveCategory))]
        public async Task<IActionResult> SaveCategory([FromForm]  MasterCategory category)
        {
            var res = new Response() { ResponseText=string.Empty,StatusCode= ResponseStatus.Failed };
            if (category.ImagePath!=null)
            {
                res = _fileUploadService.UploadImage(category.ImagePath, FileUploadPath.CategoryImage);
			
            }
            category.CategoryImage = res.ResponseText;
            res = await _categoryService.SaveOrUpdateCategory(category);
            return Ok(res); 
        }

        [HttpPost(nameof(GetCategory))]
        public async Task<IActionResult> GetCategory()
        {
            var res = await _categoryService.GetCategory();
            return Ok(res);
        }

        [HttpPost(nameof(GetCategoryById)+"/{Id}")]
        public async Task<IActionResult> GetCategoryById(int Id)
        {
            var res = await _categoryService.AddOrEditCategory(Id);
            return Ok(res);
        }
    }
}
