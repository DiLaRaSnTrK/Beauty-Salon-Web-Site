﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB3.Data;
using WEB3.Models;

namespace WEB3.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Çalışanları listele
      
        public IActionResult EmployeeOperations()
        {
            var employees = _context.employees.ToList(); // Veritabanından çalışanları al
            ViewBag.Employees = employees; // Çalışanları ViewBag'e ata

            var services = _context.services.ToList(); // Veritabanından hizmetleri al
            ViewBag.Services = services; // Services'i ViewBag'e ata

            return View();
        }
        [HttpGet("Edit/{id}")]
        public IActionResult EditEmployee(int id)
        {
            var employee = _context.employees.FirstOrDefault(e => e.employeeid == id);
            if (employee == null)
            {
                return NotFound("Çalışan bulunamadı.");
            }
            // ViewBag'e hizmetler listesini ekleyin
            ViewBag.Services = new SelectList(_context.services, "serviceid", "service");

            // Düzenleme sayfasına çalışan verisini gönder

            // Düzenleme sayfasına çalışan verisini gönder
            return View(employee);
        }


        // Yeni çalışan ekle
        [HttpPost]
        public IActionResult AddEmployee(string firstName, string lastName, int serviceId, string skills)
        {

            // Yeni çalışan oluştur
            var newEmployee = new Employees
            {
                firstname = firstName,
                lastname = lastName,
                skills = skills,
                serviceid = serviceId,
                expertise = skills,
                prolificacy = 0,

            };

            // Yeni çalışanı veritabanına ekle
            _context.employees.Add(newEmployee);
            _context.SaveChanges();

            ViewBag.SuccessMessage = "Çalışan başarıyla kaydedildi.";

            return RedirectToAction("EmployeeOperations");
        }
        [HttpPost("Edit/{id}")]
        public IActionResult EditEmployee(int id, Employees updatedEmployee)
        {
            if (id != updatedEmployee.employeeid)
            {
                return BadRequest("Geçersiz çalışan ID'si.");
            }

            var employee = _context.employees.FirstOrDefault(e => e.employeeid == id);
            if (employee == null)
            {
                return NotFound("Çalışan bulunamadı.");
            }

            // Güncellenen verileri mevcut çalışan objesine aktar
            employee.firstname = updatedEmployee.firstname;
            employee.lastname = updatedEmployee.lastname;
            employee.skills = updatedEmployee.skills;
            employee.serviceid = employee.serviceid;
            employee.expertise = updatedEmployee.expertise;

            // Değişiklikleri veritabanına kaydet
            _context.SaveChanges();

            return RedirectToAction("EmployeeOperations"); // Çalışan listeleme sayfasına yönlendir
        }
        [HttpPost("Delete/{id}")]
        [ActionName("DeleteEmployee")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.employees.FirstOrDefault(e => e.employeeid == id);
            if (employee == null)
            {
                return NotFound("Çalışan bulunamadı.");
            }

            // Çalışanı veritabanından sil
            _context.employees.Remove(employee);
            _context.SaveChanges();

            return RedirectToAction("EmployeeOperations"); // Çalışan listeleme sayfasına yönlendir
        }
     
        [HttpGet]
        public IActionResult EmployeePerformance()
        {
            var employees = _context.employees.ToList();

            if (employees == null || !employees.Any())
            {
                return View(new List<Employees>()); // Boş model gönder
            }

            var performanceData = employees.Select(employee =>
            {
                // Çalışanın randevularını çek
                var appointments = _context.appointments
                    .Where(a => a.employeeid == employee.employeeid && a.appointmentdatetime.Date == DateTime.UtcNow.Date)
                    .ToList();

                // Günlük kazanç toplamı
                var dailyEarnings = appointments.Sum(a => a.totalprice);

                // Günlük işlem süreleri toplamı
                var dailyProcessTime = appointments.Sum(a => a.process);

                // Verimlilik hesaplama
                var productivity = appointments.Sum(a => a.process) / 600.0 * 100;

                return new Employees
                {
                    employeeid = employee.employeeid,
                    EmployeeName = $"{employee.firstname} {employee.lastname}",
                    DailyEarning = dailyEarnings,
                    Productivity = productivity.ToString(),
                };
            }).ToList();

            return View(performanceData);
        }





    }
}
