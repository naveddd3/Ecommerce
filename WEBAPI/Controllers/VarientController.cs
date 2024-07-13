using Application.Interfaces;
using Domain.Entities;
using Domain.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VarientController : ControllerBase
    {
        private readonly IVarientService varientService;

        public VarientController(IVarientService varientService)
        {
            this.varientService=varientService;
        }

        [HttpPost(nameof(SaveVarient))]
        public async Task<IActionResult> SaveVarient(Varient varient)
        {
            var res = await varientService.SaveOrUpdateVarient(varient);    
            return Ok(res);
        }

        [HttpPost(nameof(GetByIdVarient)+"/{Id}")]
        public async Task<IActionResult> GetByIdVarient(int Id)
        {
            var res = await varientService.AddOrEditVarient(Id);
            return Ok(res);
        }

        [HttpPost(nameof(GetVarient))]
        public async Task<IActionResult> GetVarient()
        {
            var res = await varientService.GetAll();
            return Ok(res);
        }
    }
}
