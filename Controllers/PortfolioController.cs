using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PortfolioApi.Data;
using PortfolioApi.Models;

namespace PortfolioApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PortfolioItem>>> Get()
        {
            var items = await context.PortfolioItems.ToListAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PortfolioItem>> Get(int id)
        {
            var item = await context.PortfolioItems.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<PortfolioItem>> Post(PortfolioItem item)
        {
            context.PortfolioItems.Add(item);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PortfolioItem item)
        {  
            Console.WriteLine("id is: " + id);
            if (id != item.Id) return BadRequest();

            context.Entry(item).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
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
            var item = await context.PortfolioItems.FindAsync(id);
            if (item == null) return NotFound();

            context.PortfolioItems.Remove(item);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool PortfolioItemExists(int id)
        {
            return context.PortfolioItems.Any(e => e.Id == id);
        }
    }
}
