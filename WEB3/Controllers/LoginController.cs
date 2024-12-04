using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB3.Data; // ApplicationDbContext burada tanımlı olmalı
using WEB3.Models; // Customer ve Admin modelleri burada olmalı

namespace WEB3.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
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
            // Admin giriş kontrolü
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.username == username && a.password == password);

            if (admin != null)
            {
                // Admin giriş başarılı
                // Admin dashboard'a yönlendir
                return RedirectToAction("Dashboard", "Admin");
            }

            // Customer giriş kontrolü
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.email == username && c.password == password);

            if (customer != null)
            {
                // Kullanıcı giriş başarılı
                // Kullanıcı ana sayfasına yönlendir
                return RedirectToAction("Index", "Home");
            }

            // Eğer kullanıcı veya admin bulunmazsa hata mesajı göster
            ViewBag.Error = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }
    }
}
