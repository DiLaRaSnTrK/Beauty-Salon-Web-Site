using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB3.Data;  // DbContext sınıfı
using WEB3.Models;  // Modeller namespace
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
namespace WEB3.Controllers
{
    //[Authorize(Roles = "Customer")]
   
    public class AppointmentsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        

        [HttpGet]
        public IActionResult BookAppointment()
        {
            // Hizmetler ve çalışanlar listesini View'a gönderiyoruz
            ViewBag.Services = _context.services.ToList();
            ViewBag.Employees = _context.employees.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult BookAppointment(int ServiceId, int EmployeeId, DateTime AppointmentDateTime, int serviceduration, int serviceprice)
        {
            // Kullanıcı giriş yapmış mı kontrol et
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
                return RedirectToAction("Login", "Account");
            }

            // Çalışanın seçilen tarih ve saatte uygunluğunu kontrol et
            var appointmentStart = AppointmentDateTime.ToUniversalTime();
            var appointmentEnd = appointmentStart.AddMinutes(serviceduration);

            var isAvailable = !_context.appointments.Any(a =>
                a.employeeid == EmployeeId &&
                (
                    (a.appointmentdatetime <= appointmentStart && a.appointmentdatetime.AddMinutes(a.process) > appointmentStart) || // Yeni randevu, mevcut bir randevunun başlangıcına denk geliyor
                    (a.appointmentdatetime < appointmentEnd && a.appointmentdatetime.AddMinutes(a.process) >= appointmentEnd) ||    // Yeni randevu, mevcut bir randevunun bitişine denk geliyor
                    (a.appointmentdatetime >= appointmentStart && a.appointmentdatetime < appointmentEnd)                          // Yeni randevu, mevcut bir randevunun içine denk geliyor
                ));

            if (!isAvailable)
            {
                ViewBag.ErrorMessage = "Seçilen çalışan bu tarihte uygun değil.";
                ViewBag.Services = _context.services.ToList();
                ViewBag.Employees = _context.employees.ToList();
                return View();
            }

            // Yeni randevuyu oluştur
            var appointment = new Appointments
            {
                customerid = customerid.Value,
                serviceid = ServiceId,
                totalprice = serviceprice,
                employeeid = EmployeeId,
                process = serviceduration,
                approvalstatus = "Unapproved",
                appointmentdatetime = appointmentStart
            };

            _context.appointments.Add(appointment);
            _context.SaveChanges();

            ViewBag.SuccessMessage = "Randevunuz başarıyla oluşturuldu!";
            return RedirectToAction("BookAppointment");
        }

<<<<<<< HEAD
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
=======

>>>>>>> 49dd744f18eaa23e635ed2c80da975f5149d94bf

    }

}

