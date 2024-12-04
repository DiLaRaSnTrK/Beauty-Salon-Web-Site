using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WEB3.Models;

namespace WEB3.Data
{
    public class ApplicationDbContext : DbContext
    {
        

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Tablolarınızı temsil eden DbSet özelliklerini tanımlayın:
        public DbSet<Salon> Salons { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAvailability> EmployeeAvailabilities { get; set; }
        public DbSet<Appointments> Appointments { get; set; }
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; } // Düzeltilmiş: AppointmentStatuses
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAppointment> CustomerAppointments { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // AppointmentStatus ve Appointment ilişkisini tanımlıyoruz
            modelBuilder.Entity<AppointmentStatus>()
                .HasKey(a => a.approvalStatus);  // approvalStatus'u birincil anahtar olarak belirtiyoruz.

            modelBuilder.Entity<AppointmentStatus>()
                .HasOne(a => a.Appointments)  // appointmentId ile Appointment arasında ilişki
                .WithMany()  // Appointment, birçok AppointmentStatus'e sahip olabilir.
                .HasForeignKey(a => a.appointmentId)  // appointmentId'nin yabancı anahtar olduğunu belirtiyoruz.
                .OnDelete(DeleteBehavior.Cascade);  // Appointment silindiğinde ilgili AppointmentStatus da silinsin.

            modelBuilder.Entity<AppointmentStatus>()
                .HasOne(a => a.Admins)  // adminId ile Admin arasında ilişki
                .WithMany()  // Admin, birçok AppointmentStatus'e sahip olabilir.
                .HasForeignKey(a => a.adminId)  // adminId'nin yabancı anahtar olduğunu belirtiyoruz.
                .OnDelete(DeleteBehavior.Cascade);  // Admin silindiğinde ilgili AppointmentStatus da silinsin.

            // Employee ile ilişki
            modelBuilder.Entity<Appointments>()
                .HasOne(a => a.Employee) // Employee ile ilişki
                .WithMany()  // Employee, birçok Appointment'a sahip olabilir
                .HasForeignKey(a => a.employeeId);  // employeeId yabancı anahtar olarak kullanılır

            // Service ile ilişki
            modelBuilder.Entity<Appointments>()
                .HasOne(a => a.Service) // Service ile ilişki
                .WithMany()  // Service, birçok Appointment'a sahip olabilir
                .HasForeignKey(a => a.serviceId);  // serviceId yabancı anahtar olarak kullanılır

            // Customer ile ilişki
            modelBuilder.Entity<Appointments>()
                .HasOne(a => a.Customer) // customerId ile Customer arasında ilişki
                .WithMany()  // Customer, birçok Appointment'a sahip olabilir
                .HasForeignKey(a => a.customerId)  // customerId yabancı anahtar olarak kullanılır
                .OnDelete(DeleteBehavior.Cascade);  // Customer silindiğinde ilgili Appointment da silinsin

            // EmployeeAvailability ile Employee arasındaki ilişkiyi tanımlıyoruz
            modelBuilder.Entity<EmployeeAvailability>()
                .HasOne(ea => ea.Employee) // EmployeeAvailability ile Employee arasındaki ilişki
                .WithMany()  // Employee, birçok EmployeeAvailability'ye sahip olabilir
                .HasForeignKey(ea => ea.employeeId)  // employeeId yabancı anahtar olarak kullanılır
                .OnDelete(DeleteBehavior.Cascade);  // Employee silindiğinde ilgili EmployeeAvailability'ler de silinsin
        }
    }
}
