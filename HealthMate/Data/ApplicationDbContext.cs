using HealthMate.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HealthMate.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<BookAnAppointment> BookedAppointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Diagnosis> Diagnoses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Doctor)
                .WithMany(d => d.Prescriptions)
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); // Change to Restrict or SetNull

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Patient)
                .WithMany(p => p.Prescriptions)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Restrict); // Change to Restrict or SetNull

            modelBuilder.Entity<Prescription>()
                .HasOne(p => p.Appointment)
                .WithMany(a => a.Prescriptions)
                .HasForeignKey(p => p.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict); // Change to Restrict or SetNull

            // Configure Diagnosis relationships
            modelBuilder.Entity<Diagnosis>()
                .HasOne(d => d.Doctor)
                .WithMany(d => d.Diagnoses)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); // Change to Restrict or SetNull

            modelBuilder.Entity<Diagnosis>()
                .HasOne(d => d.Patient)
                .WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.Restrict); // Change to Restrict or SetNull

            modelBuilder.Entity<Diagnosis>()
                .HasOne(d => d.Appointment)
                .WithMany(a => a.Diagnoses)
                .HasForeignKey(d => d.AppointmentId)
                .OnDelete(DeleteBehavior.Restrict); // Change to Restrict or SetNull

            // Configure BookAnAppointment relationships
            modelBuilder.Entity<BookAnAppointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict); // Change to Restrict or SetNull

            modelBuilder.Entity<BookAnAppointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); // Change to Restrict or SetNull

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Prescription>()
        //        .HasOne(p => p.Doctor)
        //        .WithMany(d => d.Prescriptions)
        //        .HasForeignKey(p => p.DoctorId)
        //        .OnDelete(DeleteBehavior.Restrict); // Specify OnDelete behavior

        //    modelBuilder.Entity<Prescription>()
        //        .HasOne(p => p.Patient)
        //        .WithMany(p => p.Prescriptions)
        //        .HasForeignKey(p => p.PatientId)
        //        .OnDelete(DeleteBehavior.Restrict); // Specify OnDelete behavior

        //    modelBuilder.Entity<Prescription>()
        //        .HasOne(p => p.Appointment)
        //        .WithMany(a => a.Prescriptions)
        //        .HasForeignKey(p => p.AppointmentId)
        //        .OnDelete(DeleteBehavior.Restrict); // Specify OnDelete behavior

        //    base.OnModelCreating(modelBuilder);
        //}

    }
}
