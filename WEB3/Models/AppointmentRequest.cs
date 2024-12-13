namespace WEB3.Models
{
    public class AppointmentRequest
    {
        public int customerid { get; set; }
        public int serviceid { get; set; } // Seçilen işlem
        public int employeeid { get; set; } // Seçilen çalışan
        public DateTime AppointmentDateTime { get; set; } // Randevu tarihi ve saati
    }

}
