using Microsoft.AspNetCore.Mvc;

namespace HealthMate.Controllers
{
    public class RegisterLoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
