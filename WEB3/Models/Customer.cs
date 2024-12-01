namespace WEB3.Models
{
    public class Customer
    {
        public int customerId { get; set; }  // Veritabanındaki sütun adıyla uyumlu
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public int phone { get; set; }

        // Boolean olarak tanımlandı
        public bool isActive { get; set; }

        public string password { get; set; }
        public Customer(string firstName, string lastName, string email, string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
            this.isActive = true; // varsayılan değer
        }
    }
}
