using System.ComponentModel.DataAnnotations;

namespace WEB3.Models
{
    public class Services
    {
        [Key] // serviceId'nin birincil anahtar olduğunu belirtiyoruz.
        public int serviceid { get; set; }

        public string servicename { get; set; }

        public int serviceduration { get; set; } // Dakika cinsinden süre

        public int serviceprice { get; set; }    // Fiyat

        public ICollection<Appointments> appointments { get; set; }
        public ICollection<Employees> employees { get; set; } // İlişki tanımı
    }
}
