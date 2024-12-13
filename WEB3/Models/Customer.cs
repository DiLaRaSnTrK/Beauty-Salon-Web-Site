using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB3.Models
{
    public class Customer
    {
        [Key] // appointmentId'nin birincil anahtar olduğunu belirtiyoruz.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Otomatik artan olacağını belirtiyoruz
        public int customerid { get; set; }  // Veritabanındaki sütun adıyla uyumlu
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }

        // Boolean olarak tanımlandı
        public bool isactive { get; set; }

        public string password { get; set; }
        // Navigation property for Appointments
        public ICollection<Appointments> appointments { get; set; }

    }
}
