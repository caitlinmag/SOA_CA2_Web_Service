using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIService.Models;

namespace APIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksSalesController : ControllerBase
    {
        private readonly DrinksContext _context;

        public DrinksSalesController(DrinksContext context)
        {
            _context = context;
        }

        // GET: api/DrinksSales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrinksSales>>> GetDrinksSales()
        {
            return await _context.DrinksSales.ToListAsync();
        }

        // GET: api/DrinksSales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DrinksSales>> GetDrinksSales(int id)
        {
            var drinksSales = await _context.DrinksSales.FindAsync(id);

            if (drinksSales == null)
            {
                return NotFound();
            }

            return drinksSales;
        }

        // PUT: api/DrinksSales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrinksSales(int id, DrinksSales drinksSales)
        {
            if (id != drinksSales.DrinksSalesId)
            {
                return BadRequest();
            }

            _context.Entry(drinksSales).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinksSalesExists(id))
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

        // POST: api/DrinksSales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DrinksSales>> PostDrinksSales(DrinksSales drinksSales)
        {
            _context.DrinksSales.Add(drinksSales);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDrinksSales", new { id = drinksSales.DrinksSalesId }, drinksSales);
        }

        // DELETE: api/DrinksSales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrinksSales(int id)
        {
            var drinksSales = await _context.DrinksSales.FindAsync(id);
            if (drinksSales == null)
            {
                return NotFound();
            }

            _context.DrinksSales.Remove(drinksSales);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DrinksSalesExists(int id)
        {
            return _context.DrinksSales.Any(e => e.DrinksSalesId == id);
        }
    }
}
