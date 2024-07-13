using Domain.Entities;
using Domain.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEBAPP.Models.Helper;

namespace WEBAPP.Controllers
{
    public class VarientController : Controller
    {
        private readonly string _BaseUrl;

        public VarientController(AppSetting appSetting)
        {
            _BaseUrl=appSetting.WebApiBaseUrl;
        }

        [Route("MasterVarient")]
        public IActionResult MasterVarient()
        {
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            var list = new List<Varient>();
            try
            {
                var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Varient/GetVarient", null, User.GetLoggedInUserToken());
                if (apiRes != null)
                {
                    list = JsonConvert.DeserializeObject<List<Varient>>(apiRes.Result);
                }
                return PartialView(list);
            }
            catch (Exception ex)
            {

                throw;
            }
            return PartialView();
        }
        public async Task<IActionResult> AddOrEditVarient(int Id)
        {
            Varient varient = new Varient();
            try
            {
                var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Varient/GetByIdVarient/{Id}", null, User.GetLoggedInUserToken());
                if (apiRes != null)
                {
                    varient = JsonConvert.DeserializeObject<Varient>(apiRes.Result);
                }
                return PartialView(varient);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<IActionResult> SaveVarient(Varient varient)
        {
            var res = new Response() { };
            try
            {
                var apiRes = await AppWebRequest.O.PostAsync($"{_BaseUrl}/api/Varient/SaveVarient", JsonConvert.SerializeObject(varient), User.GetLoggedInUserToken());
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
