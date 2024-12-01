using System.ComponentModel.DataAnnotations.Schema;

namespace WEB3.Models
{
    public class Salon
    {
        public int salonId { get; set; }  // Veritabanındaki küçük harf ile uyumlu
        public string salonName { get; set; }  // Veritabanındaki küçük harf ile uyumlu
        public string location { get; set; }  // Veritabanındaki küçük harf ile uyumlu

        // Veritabanındaki integer değerlerini tutacağız
        public int openingTime { get; set; }  // OpeningTime veritabanındaki integer sütun
        public int closingTime { get; set; }  // ClosingTime veritabanındaki integer sütun

        // TimeSpan dönüşümü yapacak sanal özellikler
        [NotMapped]  // Bu özellik veritabanına yansıtılmayacak
        public TimeSpan OpeningTime => TimeSpan.FromMinutes(openingTime);  // Integer'dan TimeSpan'a dönüşüm

        [NotMapped]  // Bu özellik veritabanına yansıtılmayacak
        public TimeSpan ClosingTime => TimeSpan.FromMinutes(closingTime);  // Integer'dan TimeSpan'a dönüşüm
    }
}
