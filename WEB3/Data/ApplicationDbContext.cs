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
 
        public DbSet<Appointments> appointments { get; set; }
 
        public DbSet<Customer> customer { get; set; }
     
        public DbSet<Admin> admin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().ToTable("customer");

            // Admin tablosu
            modelBuilder.Entity<Admin>().ToTable("admin");

            // Employee tablosu
            modelBuilder.Entity<Employees>().ToTable("employees");



            // Appointment tablosu
            modelBuilder.Entity<Appointments>().ToTable("appointments");

      

            // Service tablosu
            modelBuilder.Entity<Services>().ToTable("services");

            // CustomerAppointments tablosu
          

           

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
