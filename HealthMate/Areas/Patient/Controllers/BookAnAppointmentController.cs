//using HealthMate.Data;
//using HealthMate.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;

//namespace HealthMate.Areas.Patient.Controllers
//{
//    [Area("Patient")]
//    [Authorize]

//    public class BookAnAppointmentController : Controller
//    {

//        private ApplicationDbContext _context;
//        private readonly UserManager<IdentityUser> _userManager;

//        public BookAnAppointmentController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }

//        public IActionResult Index()
//        {
//            //IEnumerable<BookAnAppointment> bookAnAppointments = _context.BookedAppointments;
//            var appointments = _context.BookedAppointments
//                .Include(b => b.Patient)
//                .Include(b => b.Doctor)
//                .ToList();
//            return View(appointments);
//        }

//        [HttpGet]
//        public IActionResult CreateAppointment()
//        {
//            var doctors = _context.Doctors.ToList();
//            var patients = _context.Patients.ToList();
//            ViewBag.Doctors = new SelectList(doctors, "Id", "Name"); // Assuming Doctor has Id and Name properties
//            ViewBag.Patients = new SelectList(patients, "Id", "Name"); // Assuming Patient has Id and Name properties
//            return View();
//        }

//        [HttpPost]
//        [Route("Patient/BookAnAppointment/CreateAppointment")]
//        [ValidateAntiForgeryToken]
//        public IActionResult CreateApplication(BookAnAppointment bookAnAppointment)
//        {
//            //if (ModelState.IsValid)
//            //{
//            _context.BookedAppointments.Add(bookAnAppointment);
//            _context.SaveChanges();
//            return RedirectToAction("Index");
//            //}

//            // If ModelState is not valid, reload the form with validation errors
//            //var doctors = _context.Doctors.ToList();
//            //var patients = _context.Patients.ToList();
//            //ViewBag.Doctors = new SelectList(doctors, "Id", "Name");
//            //ViewBag.Patients = new SelectList(patients, "Id", "Name");
//            //return View("CreateAppointment", bookAnAppointment);
//        }

//        [HttpGet]
//        public JsonResult GetPatientDetails(int id)
//        {
//            var patient = _context.Patients.FirstOrDefault(p => p.Id == id);
//            if (patient != null)
//            {
//                return Json(new { age = patient.Age, name = patient.Name });
//            }
//            return Json(new { age = 0, name = string.Empty });
//        }


//        [HttpGet]
//        public JsonResult GetPrescriptionDetails(int id)
//        {
//            try
//            {
//                var appointment = _context.BookedAppointments
//                    .Include(a => a.Patient)
//                    .Include(a => a.Doctor)
//                    .FirstOrDefault(a => a.Id == id);

//                if (appointment != null)
//                {
//                    var prescription = _context.Prescriptions
//                        .FirstOrDefault(p => p.AppointmentId == appointment.Id);

//                    if (prescription != null)
//                    {
//                        var diagnosis = _context.Diagnoses
//                            .FirstOrDefault(d => d.AppointmentId == appointment.Id);

//                        var data = new
//                        {
//                            prescriptionId = prescription.Id,
//                            patientName = appointment.Patient.Name,
//                            doctorName = appointment.Doctor.Name,
//                            prescriptionContent = prescription.PrescriptionInfo,
//                            nextAppointment = prescription.NextAppointment,
//                            diagnosisRequired = diagnosis?.ReferredByDoctor ?? false,
//                            diagnosisId = diagnosis?.Id ?? 0
//                        };

//                        return Json(data);
//                    }
//                    else
//                    {
//                        return Json(new { error = "Prescription not found for this appointment." });
//                    }
//                }
//                else
//                {
//                    return Json(new { error = "Appointment not found." });
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log the exception for debugging purposes
//                Console.WriteLine($"Error fetching prescription details: {ex.Message}");
//                return Json(new { error = "Error fetching prescription details." });
//            }
//        }

//        //[HttpGet]
//        //public JsonResult GetPrescriptionDetails(int id)
//        //{
//        //    try
//        //    {
//        //        var appointment = _context.BookedAppointments
//        //            .Include(a => a.Patient)
//        //            .Include(a => a.Doctor)
//        //            .FirstOrDefault(a => a.Id == id);

//        //        if (appointment != null)
//        //        {
//        //            var prescription = _context.Prescriptions
//        //                .FirstOrDefault(p => p.AppointmentId == appointment.Id);

//        //            if (prescription != null)
//        //            {
//        //                var data = new
//        //                {
//        //                    prescriptionId = prescription.Id,
//        //                    patientName = appointment.Patient.Name,
//        //                    doctorName = appointment.Doctor.Name,
//        //                    prescriptionContent = prescription.PrescriptionInfo,
//        //                    nextAppointment = prescription.NextAppointment
//        //                };

//        //                return Json(data);
//        //            }
//        //            else
//        //            {
//        //                return Json(new { error = "Prescription not found for this appointment." });
//        //            }
//        //        }
//        //        else
//        //        {
//        //            return Json(new { error = "Appointment not found." });
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        // Log the exception for debugging purposes
//        //        Console.WriteLine($"Error fetching prescription details: {ex.Message}");
//        //        return Json(new { error = "Error fetching prescription details." });
//        //    }
//        //}





//    }
//}

using HealthMate.Data;
using HealthMate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace HealthMate.Areas.Patient.Controllers
{
    [Area("Patient")]
    [Authorize]
    public class BookAnAppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BookAnAppointmentController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userEmail = user.Email;

            var appointments = _context.BookedAppointments
                .Include(b => b.Patient)
                .Include(b => b.Doctor)
                .Where(b => b.Patient.Email == userEmail) // only logged in user ki details hi show hongi
                .ToList();

            return View(appointments);
        }

        [HttpGet]
        public async Task<IActionResult> CreateAppointment()
        {
            var user = await _userManager.GetUserAsync(User);
            var patient = _context.Patients.FirstOrDefault(p => p.Email == user.Email);

            if (patient == null)
            {
                return RedirectToAction("Index"); 
            }

            var doctors = _context.Doctors.ToList();
            ViewBag.Doctors = new SelectList(doctors, "Id", "Name"); 
            ViewBag.Patient = patient; 

            return View();
        }

        [HttpPost]
        [Route("Patient/BookAnAppointment/CreateAppointment")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAppointment(BookAnAppointment bookAnAppointment)
        {
            var user = await _userManager.GetUserAsync(User);
            var patient = _context.Patients.FirstOrDefault(p => p.Email == user.Email);

            if (patient != null)
            {
                bookAnAppointment.PatientId = patient.Id;
                _context.BookedAppointments.Add(bookAnAppointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Handle the case where the patient is not found
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var patient = _context.Patients.FirstOrDefault(p => p.Email == user.Email);

            if (patient == null)
            {
                return RedirectToAction("Index");
            }

            var appointment = _context.BookedAppointments.FirstOrDefault(b => b.Id == id && b.PatientId == patient.Id && b.Status == "Pending");
            if (appointment != null)
            {
                appointment.Status = "Cancelled";
                _context.Update(appointment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }



        [HttpGet]
        public JsonResult GetPatientDetails(int id)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == id);
            if (patient != null)
            {
                return Json(new { age = patient.Age, name = patient.Name });
            }
            return Json(new { age = 0, name = string.Empty });
        }

        [HttpGet]
        public JsonResult GetPrescriptionDetails(int id)
        {
            try
            {
                var appointment = _context.BookedAppointments
                    .Include(a => a.Patient)
                    .Include(a => a.Doctor)
                    .FirstOrDefault(a => a.Id == id);

                if (appointment != null)
                {
                    var prescription = _context.Prescriptions
                        .FirstOrDefault(p => p.AppointmentId == appointment.Id);

                    if (prescription != null)
                    {
                        var diagnosis = _context.Diagnosis
                            .FirstOrDefault(d => d.AppointmentId == appointment.Id);

                        var data = new
                        {
                            prescriptionId = prescription.Id,
                            patientName = appointment.Patient.Name,
                            doctorName = appointment.Doctor.Name,
                            prescriptionContent = prescription.PrescriptionInfo,
                            nextAppointment = prescription.NextAppointment,
                            diagnosisRequired = appointment.RequiresDiagnosis,
                            diagnosisId = diagnosis?.Id ?? 0
                        };

                        return Json(data);
                    }
                    else
                    {
                        return Json(new { error = "Prescription not found for this appointment." });
                    }
                }
                else
                {
                    return Json(new { error = "Appointment not found." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Error fetching prescription details: {ex.Message}");
                return Json(new { error = "Error fetching prescription details." });
            }
        }
    }
}


