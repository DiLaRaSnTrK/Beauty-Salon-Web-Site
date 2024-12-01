using System.ComponentModel.DataAnnotations.Schema;

namespace WEB3.Models
{
    public class Appointments
    {
        public int appointmentId { get; set; }    // Veritabanındaki küçük harflerle uyumlu
        public int employeeId { get; set; }       // Veritabanındaki küçük harflerle uyumlu
        public int serviceId { get; set; }        // Veritabanındaki küçük harflerle uyumlu
        public int customerId { get; set; }       // Veritabanındaki küçük harflerle uyumlu

        // time veritabanında integer
        public int time { get; set; }

        public int process { get; set; }          // İşlem ID ya da farklı bir mantıkta kullanılabilir
        public string approvalstatus { get; set; } // Onay durumu
        public int totalprice { get; set; }       // Toplam fiyat

        // time sütununu TimeSpan'e dönüştüren sanal özellik
        [NotMapped] // Veritabanına yansıtılmaz
        public TimeSpan TimeSpanTime => TimeSpan.FromMinutes(time);
    }
}
