using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB3.Data;
using WEB3.Models;

namespace YourProjectName.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Service
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Services>>> GetServices()
        {
            return await _context.services.ToListAsync();
        }

        // GET: api/Service/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Services>> GetService(int id)
        {
            var service = await _context.services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        // POST: api/Service
        [HttpPost]
        public async Task<ActionResult<Services>> PostService(Services service)
        {
            _context.services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetService", new { id = service.serviceid }, service);
        }

        // PUT: api/Service/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, Services service)
        {
            if (id != service.serviceid)
            {
                return BadRequest();
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // DELETE: api/Service/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _context.services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceExists(int id)
        {
            return _context.services.Any(e => e.serviceid == id);
        }
    }
}

