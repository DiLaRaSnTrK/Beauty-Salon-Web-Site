using System.ComponentModel.DataAnnotations.Schema;

namespace WEB3.Models
{
    public class EmployeeAvailability
    {
        public int availabilityId { get; set; }  // Veritabanındaki küçük harf ile uyumlu
        public int employeeId { get; set; }      // Veritabanındaki küçük harf ile uyumlu

        // Veritabanında integer olarak tanımlı sütunlar
        public int startTime { get; set; }       // Veritabanındaki küçük harf ile uyumlu
        public int endTime { get; set; }         // Veritabanındaki küçük harf ile uyumlu

        // Integer değerlerini TimeSpan'e dönüştüren sanal özellikler
        [NotMapped] // Bu özellik veritabanına yansıtılmaz
        public TimeSpan StartTime => TimeSpan.FromMinutes(startTime);

        [NotMapped] // Bu özellik veritabanına yansıtılmaz
        public TimeSpan EndTime => TimeSpan.FromMinutes(endTime);
    }
}
