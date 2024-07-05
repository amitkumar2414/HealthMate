//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;

//namespace HealthMate.Models
//{
//    public class BookAnAppointment
//    {
//        [Key]
//        public int Id { get; set; }
//        public DateTime CreateDate { get; set; } = DateTime.Now;

//        public string Name { get; set; }

//        public int Age { get; set; }

//        [DataType(DataType.Date)]
//        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
//        public DateTime Date { get; set; }
//        public string Status { get; set; } = "Pending";

//        public string Description { get; set; }

//        [ForeignKey("Patient")]
//        public int PatientId { get; set; }

//        public Patient Patient { get; set; }

//        [ForeignKey("Doctor")]
//        public int DoctorId { get; set; }

//        public Doctor Doctor { get; set; }

//    }
//}


using HealthMate.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class BookAnAppointment
{
    public int Id { get; set; }
    public DateTime CreateDate { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; } = "Pending";
    public string Description { get; set; }
    public bool RequiresDiagnosis { get; set; } = false; // New field

    [ForeignKey("Patient")]
    public int PatientId { get; set; }
    public Patient Patient { get; set; }

    [ForeignKey("Doctor")]
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    public ICollection<Prescription> Prescriptions { get; set; }
    public ICollection<Diagnosis> Diagnoses { get; set; }
}


