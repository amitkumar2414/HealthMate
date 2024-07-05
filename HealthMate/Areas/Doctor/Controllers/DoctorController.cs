//using HealthMate.Data;
//using HealthMate.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Linq;
//using System.Threading.Tasks;

//namespace HealthMate.Areas.Doctor.Controllers
//{
//    [Area("Doctor")]
//    [Authorize(Roles = "Doctor")]
//    public class DoctorController : Controller
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly UserManager<IdentityUser> _userManager;

//        public DoctorController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var user = await _userManager.GetUserAsync(User);
//            var userName = user.Email;

//            var appointments = _context.BookedAppointments
//                .Include(a => a.Patient)
//                .Include(a => a.Doctor)
//                .Where(a => a.Doctor.Name == "Amit Kr. Shakya")
//                .ToList();
//            return View("Index", appointments);
//        }

//        [HttpPost]
//        public IActionResult UpdateStatus(int id, string status)
//        {
//            var appointment = _context.BookedAppointments.Find(id);
//            if (appointment != null)
//            {
//                appointment.Status = status;
//                _context.SaveChanges();
//            }
//            return Ok();
//        }

//        public IActionResult AddPrescription(int id)
//        {
//            var appointment = _context.BookedAppointments
//                .Include(a => a.Patient)
//                .Include(a => a.Doctor)
//                .FirstOrDefault(a => a.Id == id);

//            if (appointment == null || appointment.Status != "Appointed")
//            {
//                return NotFound();
//            }

//            var prescription = new Prescription
//            {
//                AppointmentId = id,
//                DoctorId = appointment.DoctorId,
//                PatientId = appointment.PatientId,
//                Doctor = appointment.Doctor,
//                Patient = appointment.Patient,
//                Appointment = appointment
//            };

//            // Debugging Information
//            Console.WriteLine("Prescription Model:");
//            Console.WriteLine($"AppointmentId: {prescription.AppointmentId}");
//            Console.WriteLine($"DoctorId: {prescription.DoctorId}");
//            Console.WriteLine($"PatientId: {prescription.PatientId}");

//            ViewBag.AppointmentId = id;
//            ViewBag.DoctorId = appointment.DoctorId;
//            ViewBag.PatientId = appointment.PatientId;

//            return View(prescription);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult AddPrescription(Prescription prescription)
//        {
//            // Log incoming data
//            Console.WriteLine("Received form submission:");
//            Console.WriteLine($"AppointmentId: {prescription.AppointmentId}");
//            Console.WriteLine($"DoctorId: {prescription.DoctorId}");
//            Console.WriteLine($"PatientId: {prescription.PatientId}");
//            Console.WriteLine($"PrescriptionInfo: {prescription.PrescriptionInfo}");
//            Console.WriteLine($"NextAppointment: {prescription.NextAppointment}");

//            // Fetching related entities to set navigation properties
//            prescription.Doctor = _context.Doctors.Find(prescription.DoctorId);
//            prescription.Patient = _context.Patients.Find(prescription.PatientId);
//            prescription.Appointment = _context.BookedAppointments.Find(prescription.AppointmentId);

//            // Ensure the related entities are attached to the context
//            _context.Entry(prescription.Doctor).State = EntityState.Unchanged;
//            _context.Entry(prescription.Patient).State = EntityState.Unchanged;
//            _context.Entry(prescription.Appointment).State = EntityState.Unchanged;

//            // Check if the entities are found
//            if (prescription.Doctor == null || prescription.Patient == null || prescription.Appointment == null)
//            {
//                ModelState.AddModelError(string.Empty, "Invalid Doctor, Patient, or Appointment.");
//                return View(prescription);
//            }

//            // Log fetched data
//            Console.WriteLine("Fetched Doctor: " + (prescription.Doctor != null ? "Yes" : "No"));
//            Console.WriteLine("Fetched Patient: " + (prescription.Patient != null ? "Yes" : "No"));
//            Console.WriteLine("Fetched Appointment: " + (prescription.Appointment != null ? "Yes" : "No"));

//            //if (ModelState.IsValid)
//            //{
//                try
//                {
//                    Console.WriteLine("Attempting to save prescription:");
//                    Console.WriteLine($"AppointmentId: {prescription.AppointmentId}");
//                    Console.WriteLine($"DoctorId: {prescription.DoctorId}");
//                    Console.WriteLine($"PatientId: {prescription.PatientId}");
//                    Console.WriteLine($"PrescriptionInfo: {prescription.PrescriptionInfo}");
//                    Console.WriteLine($"NextAppointment: {prescription.NextAppointment}");

//                    _context.Prescriptions.Add(prescription);
//                    _context.SaveChanges();
//                    Console.WriteLine("Prescription saved successfully.");
//                    return RedirectToAction("Index");
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine("Error adding prescription:");
//                    Console.WriteLine(ex.Message);
//                    return View(prescription);
//                }
//            //}
//            //else
//            //{
//            //    Console.WriteLine("Model validation failed:");
//            //    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
//            //    {
//            //        Console.WriteLine(error.ErrorMessage);
//            //    }
//            //    return View(prescription);
//            //}
//        }





//    }
//}


using HealthMate.Data;
using HealthMate.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace HealthMate.Areas.Doctor.Controllers
{
    [Area("Doctor")]
    [Authorize(Roles = "Doctor")]
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var appointments = _context.BookedAppointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.DoctorId == 1)
                .ToList();
            return View("Index", appointments);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            var appointment = _context.BookedAppointments.Find(id);
            if (appointment != null)
            {
                appointment.Status = status;
                _context.SaveChanges();
            }
            return Ok();
        }


        public IActionResult AddPrescription(int id)
        {
            var appointment = _context.BookedAppointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefault(a => a.Id == id);

            if (appointment == null || appointment.Status != "Appointed")
            {
                return NotFound();
            }

            var prescription = new Prescription
            {
                AppointmentId = id,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                Doctor = appointment.Doctor,
                Patient = appointment.Patient,
                Appointment = appointment
            };

            // Debugging Information
            Console.WriteLine("Prescription Model:");
            Console.WriteLine($"AppointmentId: {prescription.AppointmentId}");
            Console.WriteLine($"DoctorId: {prescription.DoctorId}");
            Console.WriteLine($"PatientId: {prescription.PatientId}");

            ViewBag.AppointmentId = id;
            ViewBag.DoctorId = appointment.DoctorId;
            ViewBag.PatientId = appointment.PatientId;

            return View(prescription);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPrescription(Prescription prescription)
        {
            // Log incoming data
            Console.WriteLine("Received form submission:");
            Console.WriteLine($"AppointmentId: {prescription.AppointmentId}");
            Console.WriteLine($"DoctorId: {prescription.DoctorId}");
            Console.WriteLine($"PatientId: {prescription.PatientId}");
            Console.WriteLine($"PrescriptionInfo: {prescription.PrescriptionInfo}");
            Console.WriteLine($"NextAppointment: {prescription.NextAppointment}");

            // Fetching related entities to set navigation properties
            prescription.Doctor = _context.Doctors.Find(prescription.DoctorId);
            prescription.Patient = _context.Patients.Find(prescription.PatientId);
            prescription.Appointment = _context.BookedAppointments.Find(prescription.AppointmentId);

            // Ensure the related entities are attached to the context
            _context.Entry(prescription.Doctor).State = EntityState.Unchanged;
            _context.Entry(prescription.Patient).State = EntityState.Unchanged;
            _context.Entry(prescription.Appointment).State = EntityState.Unchanged;

            // Check if the entities are found
            if (prescription.Doctor == null || prescription.Patient == null || prescription.Appointment == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Doctor, Patient, or Appointment.");
                return View(prescription);
            }

            // Log fetched data
            Console.WriteLine("Fetched Doctor: " + (prescription.Doctor != null ? "Yes" : "No"));
            Console.WriteLine("Fetched Patient: " + (prescription.Patient != null ? "Yes" : "No"));
            Console.WriteLine("Fetched Appointment: " + (prescription.Appointment != null ? "Yes" : "No"));

            //if (ModelState.IsValid)
            //{
                try
                {
                    Console.WriteLine("Attempting to save prescription:");
                    Console.WriteLine($"AppointmentId: {prescription.AppointmentId}");
                    Console.WriteLine($"DoctorId: {prescription.DoctorId}");
                    Console.WriteLine($"PatientId: {prescription.PatientId}");
                    Console.WriteLine($"PrescriptionInfo: {prescription.PrescriptionInfo}");
                    Console.WriteLine($"NextAppointment: {prescription.NextAppointment}");

                    _context.Prescriptions.Add(prescription);
                    _context.SaveChanges();
                    Console.WriteLine("Prescription saved successfully.");
                    return RedirectToAction("Index", "Doctor");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding prescription:");
                    Console.WriteLine(ex.Message);
                    return View(prescription);
                }
            //}
            //else
            //{
            //    Console.WriteLine("Model validation failed:");
            //    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            //    {
            //        Console.WriteLine(error.ErrorMessage);
            //    }
            //    return View(prescription);
            //}
        }






        [HttpPost]
        public IActionResult RequestDiagnosis(int id)
        {
            var appointment = _context.BookedAppointments.Find(id);
            if (appointment != null && appointment.Status == "Appointed")
            {
                appointment.RequiresDiagnosis = true;
                _context.SaveChanges();
            }
            return Ok();
        }
    }

}
