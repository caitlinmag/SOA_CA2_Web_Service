using APIService.Data;
using APIService.DTOs;
using APIService.Models;
using APIService.Services;
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
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkSalesController : ControllerBase
    {
    
        private readonly DrinkSaleService _salesService;

        public DrinkSalesController(DrinkSaleService salesService)
        {
            _salesService = salesService;
        }

        // GET: api/DrinkItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDTO>>> GetSales()
        {
            var sales = await _salesService.GetAllSales();

            var salesDtos = sales.Select(sale => new SaleDTO
            {
                DrinksSalesId = sale.DrinksSalesId,
                DrinkItemId = sale.DrinkItemId,
                Quantity = sale.Quantity,
                DateOfSale = sale.DateOfSale
            
            }).ToList();

            return salesDtos;
        }


        //GET: api/DrinkItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleReadDTO>> GetSale(string id)
        {
            var (sale, drink) = await _salesService.GetSaleRead(id);

            if (sale == null)
            {
                return NotFound();
            }


            var saleDto = new SaleReadDTO
            {
                DrinksSalesId = sale.DrinksSalesId,
                DrinkItemId = sale.DrinkItemId,
                Quantity = sale.Quantity,
                DateOfSale = sale.DateOfSale,

                DrinkName = drink?.DrinkName, 
                Price = drink?.Price ?? 0,
            };
            return saleDto;
        }

        // POST: api/DrinkItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DrinksSales>> PostSaleItem(SaleCreateDTO saleDto)
        {
            var newSale = new DrinksSales
            {
                DrinkItemId = saleDto.DrinkItemId,
                Quantity = saleDto.Quantity,
                DateOfSale = DateTime.UtcNow
            };

            await _salesService.CreateSale(newSale);

            var created = new SaleDTO
            {
               DrinksSalesId = newSale.DrinksSalesId,
               DrinkItemId = newSale.DrinkItemId,
               Quantity = newSale.Quantity,
               DateOfSale = newSale.DateOfSale
            };

            return CreatedAtAction(nameof(GetSales), new { id = created.DrinksSalesId }, created);
        }


        //// PUT: api/DrinkItems/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesItem(string id, SaleUpdateDTO saleDto)
        {
            var currentSale = await _salesService.GetSaleById(id);

            if (currentSale == null)
            {
                return NotFound();
            }

            currentSale.DrinkItemId = saleDto.DrinkItemId;
            currentSale.Quantity = saleDto.Quantity;

            await _salesService.UpdateSale(id, currentSale);

            return NoContent();
        }


        // DELETE: api/DrinkItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSaleItem(string id)
        {
            var currentSale = await _salesService.GetSaleById(id);

            if (currentSale == null)
            {
                return NotFound();
            }

            await _salesService.DeleteSale(id);

            return NoContent();
        }
    }
}
