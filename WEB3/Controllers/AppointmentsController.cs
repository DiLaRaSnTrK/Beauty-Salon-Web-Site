using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB3.Data;  // DbContext sınıfı
using WEB3.Models;  // Modeller namespace
using System.Linq;

namespace WEB3.Controllers
{
 
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
        public IActionResult BookAppointment(int ServiceId, int EmployeeId, DateTime AppointmentDateTime)
        {
            // Kullanıcı giriş yapmış mı kontrol et
            int? customerid = HttpContext.Session.GetInt32("Customerid");
            if (customerid == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Çalışanın seçilen tarih ve saatte uygunluğunu kontrol et
            var isAvailable = !_context.appointments.Any(a =>
                a.employeeid == EmployeeId &&
                a.AppointmentDateTime == AppointmentDateTime);

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
                employeeid = EmployeeId,
                AppointmentDateTime = AppointmentDateTime
            };

            _context.appointments.Add(appointment);
            _context.SaveChanges();

            ViewBag.SuccessMessage = "Randevunuz başarıyla oluşturuldu!";
            return RedirectToAction("BookAppointment");
        }




        /*public IActionResult MyAppointments()
        {
            int customerId = int.Parse(HttpContext.Session.GetString("CustomerId"));
            var appointments = _context.appointments
                .Where(a => a.customerid == customerId)
                .Include(a => a.employeeid)
                .ToList();
            return View(appointments);
        }*/
    }

}

