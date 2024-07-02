using Microsoft.AspNetCore.Mvc;

namespace WEBAPP.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult DoLogin()
        {
            return View();
        }
    }
}
