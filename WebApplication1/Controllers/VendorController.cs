using Microsoft.AspNetCore.Mvc;

namespace WEBAPP.Controllers
{
    public class VendorController : Controller
    {
        [Route("Vendors")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
