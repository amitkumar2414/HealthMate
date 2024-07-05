//using System.ComponentModel.DataAnnotations;

//namespace HealthMate.Models
//{
//    public class Patient
//    {
//        [Key]
//        public int Id { get; set; }

//        public string Name { get; set; }
//        public int Age { get; set; }

//        [EmailAddress]
//        public string Email { get; set; }

//        [Phone]
//        public string PhoneNo { get; set; }

//        public string Gender { get; set; }

//        public DateTime DateOfBirth { get; set; }

//        public string Address { get; set; }

//        public string City { get; set; }

//        public string State { get; set; }

//        public ICollection<BookAnAppointment> Appointments { get; set; }
//        public ICollection<Prescription> Prescriptions { get; set; }

//    }
//}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HealthMate.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNo { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public ICollection<BookAnAppointment> Appointments { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
        public ICollection<Diagnosis> Diagnoses { get; set; }

    }
}

