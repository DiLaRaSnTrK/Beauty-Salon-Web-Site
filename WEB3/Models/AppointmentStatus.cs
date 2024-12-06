using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB3.Models
{
    public class AppointmentStatus
    {
        [Key] // approvalStatus sütununu birincil anahtar olarak belirtiyoruz.
        public string approvalstatus { get; set; }

        public int appointmentid { get; set; }
        public int adminid { get; set; }

      
        //[ForeignKey("appointmentId")]            // Navigation property for Admin
       
        //[ForeignKey("adminId")]
        // Navigation property for Appointment
        //public Appointments appointments { get; set; }
        // Navigation property for Appointments
        // Navigation properties
        public Appointments appointments { get; set; }  // Appointment ile ilişki
        public Admin admin { get; set; }  // Admin ile ilişki
    }
}
