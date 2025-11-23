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
    public class DrinkItemsController : ControllerBase
    {
        private readonly DrinksContext _context;

        public DrinkItemsController(DrinksContext context)
        {
            _context = context;
        }

        // GET: api/DrinkItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrinkItem>>> GetDrinkItems()
        {
            return await _context.DrinkItems.ToListAsync();
        }

        // GET: api/DrinkItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DrinkItem>> GetDrinkItem(int id)
        {
            var drinkItem = await _context.DrinkItems.FindAsync(id);

            if (drinkItem == null)
            {
                return NotFound();
            }

            return drinkItem;
        }

        // PUT: api/DrinkItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrinkItem(int id, DrinkItem drinkItem)
        {
            if (id != drinkItem.DrinkItemId)
            {
                return BadRequest();
            }

            _context.Entry(drinkItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinkItemExists(id))
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

        // POST: api/DrinkItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DrinkItem>> PostDrinkItem(DrinkItem drinkItem)
        {
            _context.DrinkItems.Add(drinkItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDrinkItem", new { id = drinkItem.DrinkItemId }, drinkItem);
        }

        // DELETE: api/DrinkItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrinkItem(int id)
        {
            var drinkItem = await _context.DrinkItems.FindAsync(id);
            if (drinkItem == null)
            {
                return NotFound();
            }

            _context.DrinkItems.Remove(drinkItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DrinkItemExists(int id)
        {
            return _context.DrinkItems.Any(e => e.DrinkItemId == id);
        }
    }
}
