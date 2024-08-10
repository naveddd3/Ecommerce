using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WEBAPP.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        [Route("Admin")]
        [Authorize(Roles ="Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [Route("User")]
        [Authorize(Roles = "User")]
        public IActionResult User()
        {
            return View();
        }

        [Route("Vendor")]
        [Authorize(Roles = "Vendor")]
        public IActionResult Vendor()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

		public IActionResult task()
		{
			return View();
		}

		
    }
}
