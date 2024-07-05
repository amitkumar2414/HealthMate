//using System.ComponentModel.DataAnnotations;

//namespace HealthMate.Models
//{
//    public class Doctor
//    {
//        [Key]
//        public int Id { get; set; }

//        public string Name { get; set; }
//        public int Age { get; set; }

//        [EmailAddress]
//        public string Email { get; set; }

//        [Phone]
//        public string PhoneNo { get; set; }

//        public string Specialization { get; set; }

//        public string City { get; set; }

//        public string State { get; set; }

//        public string Address { get; set; }
//        public int ExperienceYears { get; set; } // Assuming experience is between 1 and 100 years
//        public ICollection<BookAnAppointment> Appointments { get; set; }
//        public ICollection<Prescription> Prescriptions { get; set; }
//        public ICollection<Patient> Patients { get; set; }

//    }
//}


using HealthMate.Models;

public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string PhoneNo { get; set; }
    public string Specialization { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Address { get; set; }
    public int ExperienceYears { get; set; }

    public ICollection<BookAnAppointment> Appointments { get; set; }
    public ICollection<Prescription> Prescriptions { get; set; }
    public ICollection<Diagnosis> Diagnoses { get; set; }
}
