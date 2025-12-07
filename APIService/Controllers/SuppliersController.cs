using APIService.DTOs;
using APIService.Models;
using APIService.Services;
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
    public class SuppliersController : ControllerBase
    {
        private readonly SupplierService _supplierService;

        public SuppliersController(SupplierService supplierService)
        {
           _supplierService = supplierService;
        }

        // GET: api/Suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetSuppliers()
        {
            var suppliers = await _supplierService.GetAllSuppliers();

            var supplierDto = suppliers.Select(supplier => new SupplierDTO
            {
               SupplierId = supplier.SupplierId,
               SupplierName = supplier.SupplierName,
               Location = supplier.Location,
               StockLevel = supplier.StockLevel,

            }).ToList();

            return supplierDto;
        }

        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDTO>> GetSupplier(string id)
        {
            var supplier = await _supplierService.GetSupplierById(id);

            if (supplier == null)
            {
                return NotFound();
            }

            var supplierDto = new SupplierDTO
            {
                SupplierId = supplier.SupplierId,
                SupplierName = supplier.SupplierName,
                Location = supplier.Location,
                StockLevel = supplier.StockLevel
            };

            return supplierDto;
        }


        // POST: api/Suppliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Supplier>> PostSupplier(SupplierCreateDTO supplierDto)
        {
            var newSupplier = new Supplier
            {
                SupplierName = supplierDto.SupplierName,
                Location = supplierDto.Location,
                StockLevel = supplierDto.StockLevel
            };

            await _supplierService.CreateSupplier(newSupplier);

            var created = new SupplierDTO
            {
               SupplierId = newSupplier.SupplierId,
               SupplierName = newSupplier.SupplierName,
               Location = newSupplier.Location,
               StockLevel = newSupplier.StockLevel
            };

            return CreatedAtAction(nameof(GetSuppliers), new { id = created.SupplierId }, created);
        }


        // PUT: api/Suppliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplier(string id, SupplierUpdateDTO supplierDto)
        {
            var currentSupplier = await _supplierService.GetSupplierById(id);

            if (currentSupplier == null)
            {
                return NotFound();
            }

            currentSupplier.SupplierName = supplierDto.SupplierName;
            currentSupplier.Location = supplierDto.Location;
            currentSupplier.StockLevel = supplierDto.StockLevel;

            await _supplierService.UpdateSupplier(id, currentSupplier);

            return NoContent();
        }

     

        // DELETE: api/Suppliers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(string id)
        {
            var currentSupplier = await _supplierService.GetSupplierById(id);

            if (currentSupplier == null)
            {
                return NotFound();
            }

            await _supplierService.DeleteSupplier(id);

            return NoContent();
        }

       
    }
}
