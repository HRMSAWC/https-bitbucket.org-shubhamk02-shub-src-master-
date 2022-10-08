using Microsoft.AspNetCore.Mvc;

namespace AWC_HRMS_Web.Controllers
{
    public class TestController : Controller
    {
        public IActionResult TestUser()
        {
            return View();
        }
    }
}
