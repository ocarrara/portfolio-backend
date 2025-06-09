using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Data;
using PortfolioApp.Models;

namespace PortfolioApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PortfolioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortfolioItem>>> Get()
        {
            var items = await _context.PortfolioItems.ToListAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PortfolioItem>> Get(int id)
        {
            var item = await _context.PortfolioItems.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<PortfolioItem>> Post(PortfolioItem item)
        {
            _context.PortfolioItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PortfolioItem item)
        {
            if (id != item.Id) return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PortfolioItemExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.PortfolioItems.FindAsync(id);
            if (item == null) return NotFound();

            _context.PortfolioItems.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PortfolioItemExists(int id)
        {
            return _context.PortfolioItems.Any(e => e.Id == id);
        }
    }
}
