using System.ComponentModel.DataAnnotations.Schema;

namespace HealthMate.Models
{
    public class Diagnosis
    {
        public int Id { get; set; }
        public double Hemoglobin { get; set; }
        public double TLC { get; set; }
        public double DLC { get; set; }
        public double RBCCount { get; set; }
        public double PlateletsCount { get; set; }
        public double BloodSugar { get; set; }
        public bool ReferredByDoctor { get; set; } = false;// New property to track referral status

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        [ForeignKey("BookAnAppointment")]
        public int AppointmentId { get; set; }
        public BookAnAppointment Appointment { get; set; }
    }
}
