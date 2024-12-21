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
        public DbSet<Services> services { get; set; }
        public DbSet<Employees> employees { get; set; }
        public DbSet<EmployeeAvailability> employeeavailability { get; set; }
        public DbSet<Appointments> appointments { get; set; }
        public DbSet<AppointmentStatus> appointmentstatus { get; set; } // Düzeltilmiş: AppointmentStatuses
        public DbSet<Customer> customer { get; set; }
        public DbSet<CustomerAppointment> customerappointments { get; set; }
        public DbSet<Admin> admin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().ToTable("customer");

            // Admin tablosu
            modelBuilder.Entity<Admin>().ToTable("admin");

            // Employee tablosu
            modelBuilder.Entity<Employees>().ToTable("employees");

            // EmployeeAvailability tablosu
            modelBuilder.Entity<EmployeeAvailability>().ToTable("employeeavailability");

            // Appointment tablosu
            modelBuilder.Entity<Appointments>().ToTable("appointments");

            // AppointmentStatus tablosu
            modelBuilder.Entity<AppointmentStatus>().ToTable("appointmentstatus");

            // Service tablosu
            modelBuilder.Entity<Services>().ToTable("services");

            // CustomerAppointments tablosu
            modelBuilder.Entity<CustomerAppointment>().ToTable("customerappointments");

            //***************************************
            modelBuilder.Entity<EmployeeAvailability>()
                .HasOne(ea => ea.employeeids)
                .WithMany()
                .HasForeignKey(ea => ea.employeeid)
                .OnDelete(DeleteBehavior.Cascade);

           

            // Appointments ve Employees arasındaki ilişki
            modelBuilder.Entity<Appointments>()
                .HasOne(a => a.employeeids)  // Appointments ile Employee arasında ilişki
                .WithMany()  // Employee, birçok Appointment'a sahip olabilir
                .HasForeignKey(a => a.employeeid)  // employeeId'nin yabancı anahtar olduğunu belirtiyoruz
                .OnDelete(DeleteBehavior.Cascade);  // Employee silindiğinde ilgili Appointment'lar da silinsin

            // Appointments ve Employees arasındaki ilişki
            modelBuilder.Entity<Appointments>()
                .HasOne(a => a.serviceids)  // Appointments ile Employee arasında ilişki
                .WithMany()  // Employee, birçok Appointment'a sahip olabilir
                .HasForeignKey(a => a.serviceid)  // employeeId'nin yabancı anahtar olduğunu belirtiyoruz
                .OnDelete(DeleteBehavior.Cascade);  // Employee silindiğinde ilgili Appointment'lar da silinsin

            // Appointments ve Customers arasındaki ilişki
            /*modelBuilder.Entity<Appointments>()
                .HasOne(a => a.customerids)  // Appointments ile Customer arasında ilişki
                .WithMany()  // Customer, birçok Appointment'a sahip olabilir
                .HasForeignKey(a => a.customerid)  // customerId'nin yabancı anahtar olduğunu belirtiyoruz
                .OnDelete(DeleteBehavior.Cascade);  // Customer silindiğinde ilgili Appointment'lar da silinsin
            
            // Appointments ve AppointmentStatus arasındaki ilişki
            modelBuilder.Entity<Appointments>()
                .HasOne(a => a.approvalstatuses)  // Appointments ile AppointmentStatus arasında ilişki
                .WithMany()  // AppointmentStatus, birçok Appointment'a sahip olabilir
                .HasForeignKey(a => a.approvalstatus)  // appointmentStatusId'nin yabancı anahtar olduğunu belirtiyoruz
                .OnDelete(DeleteBehavior.Cascade);
            // CustomerAppointments ve Customer arasındaki ilişkiyi tanımlıyoruz
            */
            modelBuilder.Entity<CustomerAppointment>()
                .HasOne(ca => ca.customer)  // CustomerAppointments ile Customer arasında ilişki
                .WithMany()  // Customer, birçok CustomerAppointments'e sahip olabilir
                .HasForeignKey(ca => ca.customerid)  // customerId'nin yabancı anahtar olduğunu belirtiyoruz
                .OnDelete(DeleteBehavior.Cascade);  // Customer silindiğinde ilgili CustomerAppointments'ler de silinsin

            // CustomerAppointments ve Services arasındaki ilişkiyi tanımlıyoruz
            modelBuilder.Entity<CustomerAppointment>()
                .HasOne(ca => ca.services)  // CustomerAppointments ile Services arasında ilişki
                .WithMany()  // Service, birçok CustomerAppointments'e sahip olabilir
                .HasForeignKey(ca => ca.serviceid)  // serviceId'nin yabancı anahtar olduğunu belirtiyoruz
                .OnDelete(DeleteBehavior.Cascade);  // Service silindiğinde ilgili CustomerAppointments'ler de silinsin

            // CustomerAppointments ve Employees arasındaki ilişkiyi tanımlıyoruz
            modelBuilder.Entity<CustomerAppointment>()
                .HasOne(ca => ca.employees)  // CustomerAppointments ile Employee arasında ilişki
                .WithMany()  // Employee, birçok CustomerAppointments'e sahip olabilir
                .HasForeignKey(ca => ca.employeeid)  // employeeId'nin yabancı anahtar olduğunu belirtiyoruz
                .OnDelete(DeleteBehavior.Cascade);  // Employee silindiğinde ilgili CustomerAppointments'ler de silinsin

            // CustomerAppointments ve Appointments arasındaki ilişkiyi tanımlıyoruz
            modelBuilder.Entity<CustomerAppointment>()
                .HasOne(ca => ca.appointments)  // CustomerAppointments ile Appointment arasında ilişki
                .WithMany()  // Appointment, birçok CustomerAppointments'e sahip olabilir
                .HasForeignKey(ca => ca.appointmentid)  // appointmentId'nin yabancı anahtar olduğunu belirtiyoruz
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Employees>()
                .HasOne(e => e.services) // Employees tablosu Services ile ilişki kurar
                .WithMany() // Services birden fazla Employee ile ilişkilidir
                .HasForeignKey(e => e.serviceid) // Foreign key alanı
                .OnDelete(DeleteBehavior.Restrict); // Silme davranışı

            modelBuilder.Entity<Employees>()
               .HasOne(e => e.services)
               .WithMany(s => s.employees)
               .HasForeignKey(e => e.serviceid)
               .OnDelete(DeleteBehavior.SetNull); // İlişki silindiğinde NULL yapar
           


        }
    }
}
