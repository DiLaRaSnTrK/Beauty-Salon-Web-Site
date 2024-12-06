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
        public string email { get; set; }
        public string expertise { get; set; }

        public int dailyearnings { get; set; }
        public string skills { get; set; }

        // Eğer availabilityId bir Foreign Key ise:
        [ForeignKey("employeeavailability")] // EmployeeAvailability tablosuna FK
        public int availabilityid { get; set; }

        public int prolificacy { get; set; }
        //public virtual ICollection<EmployeeAvailability> employeeavailability { get; set; }
        // Navigation property for EmployeeAvailability
        public EmployeeAvailability employeeavailability { get; set; }
        // Navigation property for Appointments
        public ICollection<Appointments> appointments { get; set; }
    }
}
