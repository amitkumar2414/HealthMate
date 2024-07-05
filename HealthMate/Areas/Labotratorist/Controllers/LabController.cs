using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMate.Areas.Labotratorist.Controllers
{
    [Area("Labotratorist")]
    [Authorize(Roles = "Labotratorist")]
    public class LabController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
