namespace WEB3.Models
{
    public class Admin
    {
        public int adminId { get; set; } // Admin tablosunun birincil anahtarı
        public string username { get; set; } // Kullanıcı adı (ör: ogrenci@sakarya.edu.tr)
        public string password { get; set; } // Şifre
    }
}
