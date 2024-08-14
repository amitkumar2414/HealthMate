//using HealthMate.Models;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace HealthMate.Models
//{
//    public class Diagnosis
//    {
//        public int Id { get; set; }
//        public double Hemoglobin { get; set; }
//        public double TLC { get; set; }
//        public double DLC { get; set; }
//        public double RBCCount { get; set; }
//        public double PlateletsCount { get; set; }
//        public double BloodSugar { get; set; }
//        public bool ReferredByDoctor { get; set; } = false;// New property to track referral status

//        [ForeignKey("Doctor")]
//        public int DoctorId { get; set; }
//        public Doctor Doctor { get; set; }

//        [ForeignKey("Patient")]
//        public int PatientId { get; set; }
//        public Patient Patient { get; set; }

//        [ForeignKey("BookAnAppointment")]
//        public int AppointmentId { get; set; }
//        public BookAnAppointment Appointment { get; set; }
//    }
//}

using System.ComponentModel.DataAnnotations.Schema;

namespace HealthMate.Models
{
    public class Diagnosis
    {
        public int Id { get; set; }

        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public bool ReferredByDoctor { get; set; }

        public decimal Hemoglobin { get; set; }
        public decimal TLC { get; set; }
        public decimal DLC { get; set; }
        public decimal RBCCount { get; set; }
        public decimal PlateletsCount { get; set; }
        public decimal BloodSugar { get; set; }

        public virtual BookAnAppointment Appointment { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
