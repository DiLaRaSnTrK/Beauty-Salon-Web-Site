using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
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
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAppointment> CustomerAppointments { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API ilişkiler ve diğer yapılandırmalar burada yapılabilir
        }
    }
}
