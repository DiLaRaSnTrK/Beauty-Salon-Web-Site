using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB3.Data;  // DbContext sınıfı
using WEB3.Models;  // Modeller namespace

namespace WEB3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentStatusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AppointmentStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentStatus>>> GetAppointmentStatuses()
        {
            return await _context.AppointmentStatuses.ToListAsync();
        }

        // GET: api/AppointmentStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentStatus>> GetAppointmentStatus(int id)
        {
            var status = await _context.AppointmentStatuses.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return status;
        }

        // POST: api/AppointmentStatus
        [HttpPost]
        public async Task<ActionResult<AppointmentStatus>> PostAppointmentStatus(AppointmentStatus status)
        {
            _context.AppointmentStatuses.Add(status);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAppointmentStatus), new { id = status.approvalStatus }, status);
        }

        // PUT: api/AppointmentStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointmentStatus(int id, AppointmentStatus status)
        {
            // Öncelikle verilen id ile mevcut olan AppointmentStatus'u buluyoruz
            var existingStatus = await _context.AppointmentStatuses.FindAsync(id);

            if (existingStatus == null)
            {
                return NotFound(); // Eğer bulunamazsa, 404 dönüyoruz
            }

            // Veritabanındaki veriyi güncelliyoruz
            existingStatus.approvalStatus = status.approvalStatus; // approvalStatus güncellenir

            // Değişiklikleri kaydediyoruz
            _context.Entry(existingStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentStatusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Başarılı bir şekilde güncelleme yapıldığında 204 No Content döner
        }

        // DELETE: api/AppointmentStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointmentStatus(int id)
        {
            var status = await _context.AppointmentStatuses.FindAsync(id);
            if (status == null)
            {
                return NotFound(); // Eğer AppointmentStatus bulunamazsa, 404 döner
            }

            // AppointmentStatus'u silme işlemi
            _context.AppointmentStatuses.Remove(status);
            await _context.SaveChangesAsync();

            return NoContent(); // Silme başarılı olduğunda 204 döner
        }

        // Helper metod: AppointmentStatus var mı diye kontrol ediyor
        private bool AppointmentStatusExists(int id)
        {
            // id'nin approvalStatus ile eşleşip eşleşmediğini kontrol et
            return _context.AppointmentStatuses.Any(e => e.approvalStatus == id.ToString());
        }

    }
}
