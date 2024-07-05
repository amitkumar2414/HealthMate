using Microsoft.AspNetCore.Mvc;

namespace HealthMate.Controllers
{
    [Area("Patient")]
    public class BloodAvailabilityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
