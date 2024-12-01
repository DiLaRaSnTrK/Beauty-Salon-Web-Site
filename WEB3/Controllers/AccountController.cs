using Microsoft.AspNetCore.Mvc;

namespace WEB3.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            // Giriş işlemleri burada yapılır.
            // Örnek: Veritabanından kullanıcıyı kontrol et.
            return RedirectToAction("Index", "Home");
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



