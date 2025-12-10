using APIService.Data;
using APIService.DTOs;
using APIService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkItemsController : ControllerBase { 
    
        private readonly DrinksService _drinksService;

        public DrinkItemsController(DrinksService drinksService)
        {
            _drinksService = drinksService;
        }

        // GET: api/DrinkItems
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DrinkDTO>>> GetDrinks()
        {
            var drinks = await _drinksService.GetAllDrinks();

            var drinksDtos = drinks.Select(drink => new DrinkDTO
            {
                DrinkItemId = drink.DrinkItemId,
                DrinkName = drink.DrinkName,
                DrinkType = drink.DrinkType,
                Price = drink.Price,
                Extras = drink.Extras, 
                SupplierId = drink.SupplierId
            }).ToList();

            return drinksDtos;
        }


        //GET: api/DrinkItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DrinkReadDTO>> GetDrinkItem(string id)
        {
            var (drinkItem, supplier) = await _drinksService.GetDrinkRead(id);

            if (drinkItem == null)
            {
                return NotFound();
            }

            var drinkDto = new DrinkReadDTO
            {
                DrinkItemId = drinkItem.DrinkItemId,
                DrinkName = drinkItem.DrinkName,
                DrinkType = drinkItem.DrinkType,
                Price = drinkItem.Price,
                Extras = drinkItem.Extras,
                SupplierId = drinkItem.SupplierId,
                SupplierName = supplier?.SupplierName,
                Location = supplier?.Location           
            };
            return drinkDto;
        }

        // POST: api/DrinkItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DrinkDTO>> PostDrinkItem([FromBody]DrinkCreateDTO drinkDto)
        {
            var newDrink = new DrinkItem
            {
                DrinkName = drinkDto.DrinkName,
                DrinkType = drinkDto.DrinkType,
                Price = drinkDto.Price,
                Extras = drinkDto.Extras,
                SupplierId = drinkDto.SupplierId
            };

            await _drinksService.CreateDrink(newDrink);

            var created = new DrinkDTO
            {
                DrinkItemId = newDrink.DrinkItemId,
                DrinkName = newDrink.DrinkName,
                DrinkType = newDrink.DrinkType,
                Price = newDrink.Price,
                Extras = newDrink.Extras
            };

            return CreatedAtAction(nameof(GetDrinkItem), new { id = created.DrinkItemId }, created);
        }


        //// PUT: api/DrinkItems/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrinkItem(string id, [FromBody] DrinkUpdateDTO drinkDto)
        {
            var currentDrink = await _drinksService.GetDrinkByID(id);

            if(currentDrink == null)
            {
                return NotFound();
            }

            currentDrink.DrinkName = drinkDto.DrinkName;
            currentDrink.DrinkType = drinkDto.DrinkType;
            currentDrink.Price = drinkDto.Price;
            currentDrink.Extras = drinkDto.Extras;
            currentDrink.SupplierId = drinkDto.SupplierId;


            await _drinksService.UpdateDrink(id, currentDrink);

            return NoContent();
        }


        // DELETE: api/DrinkItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrinkItem(string id)
        {
            var currentDrink = await _drinksService.GetDrinkByID(id);
               
            if (currentDrink == null)
            {
                return NotFound();
            }

            await _drinksService.DeleteDrink(id);

            return NoContent();
        }
    }
}
