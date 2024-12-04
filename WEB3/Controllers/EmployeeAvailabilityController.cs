using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB3.Data;
using WEB3.Models;

namespace WEB3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAvailabilityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeAvailabilityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeAvailability
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeAvailability>>> GetEmployeeAvailabilities()
        {
            return await _context.EmployeeAvailabilities.ToListAsync();
        }

        // GET: api/EmployeeAvailability/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeAvailability>> GetEmployeeAvailability(int id)
        {
            var availability = await _context.EmployeeAvailabilities.FindAsync(id);

            if (availability == null)
            {
                return NotFound();
            }

            return availability;
        }

        // POST: api/EmployeeAvailability
        [HttpPost]
        public async Task<ActionResult<EmployeeAvailability>> PostEmployeeAvailability(EmployeeAvailability availability)
        {
            _context.EmployeeAvailabilities.Add(availability);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeAvailability", new { id = availability.availabilityId }, availability);
        }

        // PUT: api/EmployeeAvailability/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeAvailability(int id, EmployeeAvailability availability)
        {
            if (id != availability.availabilityId)
            {
                return BadRequest();
            }

            _context.Entry(availability).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeAvailabilityExists(id))
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

        // DELETE: api/EmployeeAvailability/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAvailability(int id)
        {
            var availability = await _context.EmployeeAvailabilities.FindAsync(id);
            if (availability == null)
            {
                return NotFound();
            }

            _context.EmployeeAvailabilities.Remove(availability);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeAvailabilityExists(int id)
        {
            return _context.EmployeeAvailabilities.Any(e => e.availabilityId == id);
        }
    }
}
