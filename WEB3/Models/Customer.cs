namespace WEB3.Models
{
    public class Customer
    {
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
