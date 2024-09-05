using Domain.Entities;
using Domain.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEBAPP.Models.Helper;

namespace WEBAPP.Controllers
{
    public class UnitController : Controller
    {
        private readonly string _BaseUrl;

        public UnitController(AppSetting appSetting)
        {
            _BaseUrl=appSetting.WebApiBaseUrl;
        }

        [Route("MasterUnit")]
        public IActionResult MasterUnit()
        {
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            var list = new List<MasterUnit>();
            try
            {
                var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Unit/GetUnit", null, User.GetLoggedInUserToken());
                if (apiRes != null)
                {
                    list = JsonConvert.DeserializeObject<List<MasterUnit>>(apiRes.Result);
                }
                return PartialView(list);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IActionResult> AddOrEditUnit(int Id)
        {
            MasterUnit Unit = new MasterUnit();
            try
            {
                var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Unit/GetByIdUnit/{Id}", null, User.GetLoggedInUserToken());
                if (apiRes != null)
                {
                    Unit = JsonConvert.DeserializeObject<MasterUnit>(apiRes.Result);
                }
                return PartialView(Unit);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IActionResult> SaveUnit(MasterUnit Unit)
        {
            var res = new Response() { };
            try
            {
                var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Unit/SaveUnit", JsonConvert.SerializeObject(Unit), User.GetLoggedInUserToken());
                if (apiRes != null)
                {
                    res = JsonConvert.DeserializeObject<Response>(apiRes.Result);   
                }
                return Json(res);
            }
            catch (Exception ex)
            {
                res.ResponseText = ex.Message;
                res.StatusCode = ResponseStatus.Failed;
                return Json(res);
            }
        }
    }
}
