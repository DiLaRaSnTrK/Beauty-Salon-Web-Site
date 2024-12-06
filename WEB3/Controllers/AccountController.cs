using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB3.Data;

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
        public IActionResult Login(string username, string password)
        {
            // Veritabanında username ve password eşleşmesini kontrol ediyoruz
            var customer = _context.customer
                .FirstOrDefault(c => c.email == username && c.password == password);

            var admin = _context.admin
                    .FirstOrDefault(a => a.username == username && a.password == password);

            if (customer != null)
            {
                // Eğer müşteri bulunduysa admin olup olmadığını kontrol ediyoruz


                // Eğer admin değilse Profile sayfasına yönlendir
                return RedirectToAction("Profile", "Account");
            }
            else if (admin != null)
            {
                // Eğer adminse AdminController'a yönlendir
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                // Eşleşme yoksa hata mesajı ile tekrar Login sayfasına dön
                ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı.";
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string name, string email, string password)
        {
            // Kayıt işlemleri burada yapılır.
            return RedirectToAction("Login");
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}



