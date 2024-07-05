using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HealthMate.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public string Gender { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        //public ICollection<BookAnAppointment> Appointments { get; set; }
        //public ICollection<Prescription> Prescriptions { get; set; }
        //public ICollection<Diagnosis> Diagnoses { get; set; }
    }
}
