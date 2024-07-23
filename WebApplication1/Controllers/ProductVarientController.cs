using Microsoft.AspNetCore.Mvc;

namespace WEBAPP.Controllers
{
    public class ProductVarientController : Controller
    {
        [Route("ProductVarient")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
