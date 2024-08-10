using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEBAPP.Controllers
{
    [Authorize(Roles = "Vendor")]
    public class VendorController : Controller
    {
      
    }
}
