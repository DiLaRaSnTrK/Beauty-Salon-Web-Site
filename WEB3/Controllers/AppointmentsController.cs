using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB3.Data;  // DbContext sınıfı
using WEB3.Models;  // Modeller namespace

namespace WEB3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            return View();
        }

        [HttpPost]
        public IActionResult BookAppointment(CustomerAppointment customerAppointment)
        {
            if (ModelState.IsValid)
            {
                // Randevuyu veritabanına kaydetme işlemi
                TempData["Message"] = "Randevunuz başarıyla alındı!";
                return RedirectToAction("MyAppointments");
            }

            TempData["Error"] = "Randevu alırken bir hata oluştu!";
            return View();
        }
        // Kullanıcının randevularını listele
        [HttpGet]
        public IActionResult MyAppointments()
        {
            var userId = User.Identity.Name;

            // Kullanıcının randevularını getir
            var appointments = _context.CustomerAppointments
                .Include(ca => ca.Appointment)
                .Include(ca => ca.Customer)
                .Where(ca => ca.Customer.email == userId)
                .ToList();

            return View(appointments);
        }
        // Yeni Randevu Sayfası
        /*public IActionResult Create()
        {
            // ViewBag ile salon ve hizmet listelerini doldur
            ViewBag.Salons = _context.Salons.ToList();
            ViewBag.Services = _context.Services.ToList();
            ViewBag.Employees = _context.Employees.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Appointments appointment)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcı bilgisini ekle
                appointment.customerId = (int)(_context.Customers
                    .FirstOrDefault(c => c.email == User.Identity.Name)?.customerId);

                // Randevuyu kaydet
                _context.Appointments.Add(appointment);
                _context.SaveChanges();

                return RedirectToAction("Dashboard", "Customer");
            }
            return View(appointment);
        }*/

        // GET: api/Appointment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointments>>> GetAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }

        // GET: api/Appointment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Appointments>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        // POST: api/Appointment
        [HttpPost]
        public async Task<ActionResult<Appointments>> PostAppointment(Appointments appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.appointmentId }, appointment);
        }

        // PUT: api/Appointment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, Appointments appointment)
        {
            if (id != appointment.appointmentId)
            {
                return BadRequest();
            }

            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Appointment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.appointmentId == id);
        }
    }
}
