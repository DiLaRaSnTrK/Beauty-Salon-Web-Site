using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB3.Models;  // Employee modelinin bulunduğu namespace
using WEB3.Data;

namespace WEB3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyAppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyAppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult MyAppointments()
        {
            
            return View();  // EmployeeOperations.cshtml sayfasını render eder
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointments>>> GetAppointments()
        {
            // Oturumdan CustomerId değerini al
            int? customerId = HttpContext.Session.GetInt32("CustomerId");

            // Eğer CustomerId yoksa, yetkisiz erişim veya hata mesajı döndür
            if (customerId == null)
            {
                return Unauthorized();  // Eğer oturum yoksa, 401 döndür
            }
            // Randevuları customerId'ye göre al
            var appointments = await _context.appointments
                .Where(a => a.customerid == customerId)
                .Select(a => new Appointments
                {
                    appointmentid = a.appointmentid,
                    employeeid = a.employeeid,
                    serviceid = a.serviceid,
                    appointmentdatetime = a.appointmentdatetime,
                    totalprice = a.totalprice,
                    process = a.process,
                    approvalstatus = a.approvalstatus
                })
                .ToListAsync();
            // Randevuları customerId'ye göre al
            //var appointments = await _context.appointments
            //    .Where(a => a.customerid == customerId)
            //    .ToListAsync();

            // Eğer randevular bulunamazsa, 404 döndür
            if (appointments == null || !appointments.Any())
            {
                return NotFound();  // Randevu bulunamadığında 404 döndür
            }

            // Randevuları döndür
            return Ok(appointments);
        }


    }
}
