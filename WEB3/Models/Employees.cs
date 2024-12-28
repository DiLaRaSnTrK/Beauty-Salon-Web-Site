using System.ComponentModel.DataAnnotations; // Key özniteliğini kullanabilmek için
using System.ComponentModel.DataAnnotations.Schema;


namespace WEB3.Models
{
    public class Employees
    {
        [Key] // employeeId sütunu Primary Key
        public int employeeid { get; set; }

        public string firstname { get; set; }
        public string lastname { get; set; }
      
        public string? expertise { get; set; }

        public int dailyearnings { get; set; }
        public string? skills { get; set; }

        // Eğer availabilityId bir Foreign Key ise:
        [ForeignKey("employeeavailability")] // EmployeeAvailability tablosuna FK

        public int serviceid { get; set; }
        public decimal? prolificacy { get; set; } // Nullable int

  
        // Navigation property for EmployeeAvailability
        
        // Navigation property for Appointments
        public ICollection<Appointments> appointments { get; set; }
        public Services services { get; set; }  // Employees tablosunda hangi hizmeti verdiğini göstermek için


        [NotMapped]
        public int DailyEarning { get; set; }
        [NotMapped]
        public string EmployeeName { get; set; }
        [NotMapped]
        public string Productivity { get; set; }
    }
}
