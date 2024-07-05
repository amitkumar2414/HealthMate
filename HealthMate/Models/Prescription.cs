using HealthMate.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Prescription
{
    public int Id { get; set; }

    [Required]
    public string PrescriptionInfo { get; set; }

    public string NextAppointment { get; set; }

    [Required]
    [ForeignKey("Doctor")]
    public int DoctorId { get; set; }
    public virtual Doctor Doctor { get; set; }

    [Required]
    [ForeignKey("Patient")]
    public int PatientId { get; set; }
    public virtual Patient Patient { get; set; }

    [Required]
    [ForeignKey("BookAnAppointment")]
    public int AppointmentId { get; set; }
    public virtual BookAnAppointment Appointment { get; set; }

}
