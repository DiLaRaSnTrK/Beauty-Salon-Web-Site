using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB3.Models
{
    public class AppointmentStatus
    {
        [Key] // approvalStatus sütununu birincil anahtar olarak belirtiyoruz.
        public string approvalStatus { get; set; }

        public int appointmentId { get; set; }
        public int adminId { get; set; }

        // Yabancı anahtar ilişkileri
        [ForeignKey("appointmentId")]
        public virtual Appointments Appointments { get; set; }  // AppointmentStatus ile Appointments arasındaki ilişkiyi belirtiyoruz.

        [ForeignKey("adminId")]
        public virtual Admin Admins { get; set; }  // AppointmentStatus ile Admin arasındaki ilişkiyi belirtiyoruz.
    }
}
