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
        // Tüm çalışanları listeleme (GET /api/employee)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointments>>> GetAppointments()
        {
            var appointment = await _context.appointments.ToListAsync();
            return Ok(appointment);  // Çalışanları döndür
        }

        // Çalışanı ID ile alma (GET /api/employee/{id})
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointments>> GetAppointments(int id)
        {
            var appointment = await _context.appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();  // Eğer çalışan bulunmazsa, 404 döndür
            }

            return Ok(appointment);  // Çalışanı döndür
        }

    }
}
