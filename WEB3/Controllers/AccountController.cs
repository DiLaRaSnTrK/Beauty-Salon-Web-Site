using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB3.Data;
using WEB3.Models;

namespace WEB3.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Kullanıcıyı kontrol et
            var customer = _context.customer
                .FirstOrDefault(c => c.email == username && c.password == password);
            var admin = _context.admin
                .FirstOrDefault(a => a.username == username && a.password == password);

            if (customer != null)
            {
                // Giriş yapan kullanıcı bilgilerini Session'da saklamak
                HttpContext.Session.SetInt32("CustomerId", customer.customerid);

                // Profil sayfasına yönlendiriyoruz
                return RedirectToAction("Profile", "Account");
            }
            else if (admin != null)
            {
                // Eğer adminse AdminController'a yönlendir
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı.";
                return View();
            }
        }
        [HttpPost]
        public IActionResult LogOut()
        {
            // Oturumu sonlandır
            HttpContext.Session.Clear(); // Tüm session verilerini temizler

            // Ana sayfaya yönlendir
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string firstName, string lastName, string email, string phone, string password)
        {
            // Aynı e-posta ile kayıtlı kullanıcı olup olmadığını kontrol et
            var existingCustomer = _context.customer
                .FirstOrDefault(c => c.email == email);

            if (existingCustomer != null)
            {
                ViewBag.ErrorMessage = "Bu e-posta adresi ile zaten bir hesap bulunmaktadır.";
                return View(); // Hata mesajı ile aynı sayfayı yeniden yükler
            }

            // Yeni müşteri oluştur
            var newCustomer = new Customer
            {
                firstname = firstName,
                lastname = lastName,
                email = email,
                password = password,  // Şifreyi burada düz şekilde kaydediyoruz. Güvenlik için şifreyi hashleyin.
                isactive = true // Kullanıcı aktif olarak işaretlenebilir.
            };

            // Yeni kullanıcıyı veritabanına ekle
            _context.customer.Add(newCustomer);
            _context.SaveChanges();
            ViewBag.SuccessMessage = "Kayıt işleminiz başarıyla tamamlandı! Lütfen giriş yapın.";

            return View();
        }

        public IActionResult Profile()
        {
            // Session'dan müşteri bilgilerini alıyoruz
            var customerId = HttpContext.Session.GetInt32("CustomerId");
            var customerName = HttpContext.Session.GetString("CustomerName");
            var customerSurname = HttpContext.Session.GetString("CustomerSurname");
            var customerEmail = HttpContext.Session.GetString("CustomerEmail");


            // Bilgileri View'da gösterebilmek için ViewBag kullanıyoruz
            ViewBag.customerid = customerId;
            ViewBag.firstname = customerName;
            ViewBag.lastname = customerSurname;
            ViewBag.email = customerEmail;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BookAppointment(int serviceId, int employeeId, DateTime appointmentDateTime)
        {
            // Giriş yapan kullanıcının customerId'sini alıyoruz
            int? customerid = HttpContext.Session.GetInt32("CustomerId");
            if (customerid.HasValue)
            {
                Console.WriteLine($"CustomerId: {customerid.Value}");
            }
            else
            {
                Console.WriteLine("CustomerId bulunamadı.");
            }

            if (customerid == null)
            {
                // Giriş yapmamış kullanıcıyı giriş sayfasına yönlendiriyoruz
                return RedirectToAction("Login", "Account");
            }

            // Randevu bilgilerini API'ye göndermek için nesne oluşturuyoruz
            var appointmentRequest = new AppointmentRequest
            {
                customerid = customerid.Value,
                serviceid = serviceId,
                employeeid = employeeId,
                AppointmentDateTime = appointmentDateTime
            };

            // API'ye veri göndermek için HttpClient kullanıyoruz
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://yourapiurl/api/appointments"); // API base adresinizi buraya ekleyin
                var response = await client.PostAsJsonAsync("book", appointmentRequest);

                if (response.IsSuccessStatusCode)
                {
                    // Randevu başarıyla alındı
                    ViewBag.SuccessMessage = "Randevunuz başarıyla alındı!";
                    return RedirectToAction("AppointmentSuccess"); // Başarı sayfasına yönlendirme
                }
                else
                {
                    // API'den hata mesajı alındığında
                    ViewBag.ErrorMessage = "Randevu alırken bir hata oluştu.";
                    return View();
                }
            }

        }

    }
}



