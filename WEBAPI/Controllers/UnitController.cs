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
    public class UnitController : ControllerBase
    {
        private readonly IUnitService unitService;

        public UnitController(IUnitService unitService)
        {
            this.unitService=unitService;
        }

        [HttpPost(nameof(SaveUnit))]
        public async Task<IActionResult> SaveUnit(MasterUnit unit)
        {
            var res = await unitService.SaveOrUpdateUnit(unit);    
            return Ok(res);
        }

        [HttpPost(nameof(GetByIdUnit) +"/{Id}")]
        public async Task<IActionResult> GetByIdUnit(int Id)
        {
            var res = await unitService.AddOrEditUnit(Id);
            return Ok(res);
        }

        [HttpPost(nameof(GetUnit))]
        public async Task<IActionResult> GetUnit()
        {
            var res = await unitService.GetAll();
            return Ok(res);
        }
    }
}
