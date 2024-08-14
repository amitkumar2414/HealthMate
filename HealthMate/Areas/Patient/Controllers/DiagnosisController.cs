using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthMate.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HealthMate.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Authorize]
    public class DiagnosisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiagnosisController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            // Find the patient associated with the logged-in user's email
            var patient = _context.Patients.FirstOrDefault(p => p.Email == userEmail);

            if (patient == null)
            {
                return NotFound("Patient not found.");
            }

            // Get diagnoses for the logged-in patient
            var diagnoses = _context.Diagnosis
                .Include(d => d.Patient)
                .Include(d => d.Doctor)
                .Include(d => d.Appointment)
                .Where(d => d.PatientId == patient.Id)
                .ToList();

            return View(diagnoses);

            //var diagnoses = _context.Diagnoses
            //    .Include(d => d.Patient)
            //    .Include(d => d.Doctor)
            //    .Include(d => d.Appointment)
            //    .ToList();
            //return View(diagnoses);
        }

        [HttpGet]
        public JsonResult GetDiagnosisDetails(int id)
        {
            try
            {
                var diagnosis = _context.Diagnosis
                    .Include(d => d.Patient)
                    .Include(d => d.Doctor)
                    .FirstOrDefault(d => d.Id == id);

                if (diagnosis != null)
                {
                    var data = new
                    {
                        diagnosisId = diagnosis.Id,
                        patientName = diagnosis.Patient.Name,
                        doctorName = diagnosis.Doctor.Name,
                        hemoglobin = diagnosis.Hemoglobin,
                        tlc = diagnosis.TLC,
                        dlc = diagnosis.DLC,
                        rbcCount = diagnosis.RBCCount,
                        plateletsCount = diagnosis.PlateletsCount,
                        bloodSugar = diagnosis.BloodSugar,
                        referredByDoctor = diagnosis.Doctor.Name // Return the doctor's name here
                    };

                    return Json(data);
                }
                else
                {
                    return Json(new { error = "Diagnosis not found." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Error fetching diagnosis details: {ex.Message}");
                return Json(new { error = "Error fetching diagnosis details." });
            }
        }

    }
}
