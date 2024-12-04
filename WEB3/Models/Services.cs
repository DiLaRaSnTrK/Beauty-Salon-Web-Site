using System.ComponentModel.DataAnnotations;

namespace WEB3.Models
{
    public class Services
    {
        [Key] // serviceId'nin birincil anahtar olduğunu belirtiyoruz.
        public int serviceId { get; set; }

        [Required] // serviceName zorunlu alan.
        public string serviceName { get; set; }

        public int serviceDuration { get; set; } // Dakika cinsinden süre

        public int servicePrice { get; set; }    // Fiyat
    }
}
