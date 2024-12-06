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
            return await _context.employeeavailability.ToListAsync();
        }

        // GET: api/EmployeeAvailability/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeAvailability>> GetEmployeeAvailability(int id)
        {
            var availability = await _context.employeeavailability.FindAsync(id);

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
            _context.employeeavailability.Add(availability);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeAvailability", new { id = availability.availabilityid }, availability);
        }

        // PUT: api/EmployeeAvailability/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeAvailability(int id, EmployeeAvailability availability)
        {
            if (id != availability.availabilityid)
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
            var availability = await _context.employeeavailability.FindAsync(id);
            if (availability == null)
            {
                return NotFound();
            }

            _context.employeeavailability.Remove(availability);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeAvailabilityExists(int id)
        {
            return _context.employeeavailability.Any(e => e.availabilityid == id);
        }
    }
}
