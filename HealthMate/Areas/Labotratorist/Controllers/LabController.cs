using HealthMate.Data;
using HealthMate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HealthMate.Areas.Labotratorist.Controllers
{
    [Area("Labotratorist")]
    [Authorize(Roles = "Labotratorist")]
    public class LabController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LabController> _logger;

        public LabController(ApplicationDbContext context, ILogger<LabController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var diagnoses = await _context.BookedAppointments
                .Include(d => d.Doctor)
                .Include(d => d.Patient)
                .Where(d => d.RequiresDiagnosis == true)
                .ToListAsync();

            return View(diagnoses);
        }

        public IActionResult GenerateReport(int id)
        {
            var diagnosis = _context.BookedAppointments
                .Include(d => d.Doctor)
                .Include(d => d.Patient)
                .FirstOrDefault(a => a.Id == id);

            if (diagnosis == null)
            {
                return NotFound();
            }

            var generateReport = new Diagnosis
            {
                AppointmentId = id,
                DoctorId = diagnosis.DoctorId,
                PatientId = diagnosis.PatientId,
            };

            _logger.LogInformation("Initial Load of GenerateReport: {@GenerateReport}", generateReport);

            return View(generateReport);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateReport(Diagnosis diagnosis)
        {
            _logger.LogInformation("GenerateReport POST method hit");

            _logger.LogInformation("Received Diagnosis Model: {@Diagnosis}", diagnosis);

            // Ensure the primary key (Id) is not set manually
            diagnosis.Id = 0;

            _context.Diagnosis.Add(diagnosis);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Diagnosis saved to database");

            return RedirectToAction("Index", "Lab");
        }



    }
}