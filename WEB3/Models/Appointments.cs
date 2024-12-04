using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB3.Models
{
    public class Appointments
    {
        [Key] // appointmentId'nin birincil anahtar olduğunu belirtiyoruz.
        public int appointmentId { get; set; }

        // Yabancı Anahtarlar
        public int employeeId { get; set; }    // employeeId'nin, Employee tablosuyla ilişkilendirileceğini belirtmeliyiz
        public int serviceId { get; set; }     // serviceId'nin, Service tablosuyla ilişkilendirileceğini belirtmeliyiz
        public int customerId { get; set; }    // customerId'nin, Customer tablosuyla ilişkilendirileceğini belirtmeliyiz

        public int time { get; set; }          // Zaman verisi (dakika cinsinden)
        public int process { get; set; }       // İşlem ID ya da başka bir mantık
        public string approvalstatus { get; set; }  // Onay durumu
        public int totalprice { get; set; }    // Toplam fiyat

        // time sütununu TimeSpan'e dönüştüren sanal özellik
        [NotMapped] // Veritabanına yansıtılmaz
        public TimeSpan TimeSpanTime => TimeSpan.FromMinutes(time);

        // Navigation properties (yabancı anahtar ilişkileri)
        public virtual Employee Employee { get; set; }
        public virtual Services Service { get; set; }
        public virtual Customer Customer { get; set; }  // Customer ile ilişkiyi burada belirtiyoruz

        // AppointmentStatus ilişkisi
        public virtual AppointmentStatus approvalStatus { get; set; }
    }
}
