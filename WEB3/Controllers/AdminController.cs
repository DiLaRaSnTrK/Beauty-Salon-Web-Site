
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEB3.Controllers
{

    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            // Hizmet yönetimi sayfası.
            return View();
        }
        public IActionResult ManageServices()
        {
            // Hizmet yönetimi sayfası.
            return View();
        }

        public IActionResult ManageEmployees()
        {
            // Çalışan yönetimi sayfası.
            return View();
        }

        public IActionResult ManageAppointments()
        {
            // Randevu yönetimi sayfası.
            return View();
        }

        public IActionResult ManageSalons()
        {
            // Salon yönetimi sayfası.
            return View();
        }
    }

}
