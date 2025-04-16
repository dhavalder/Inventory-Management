using Inventory_Management_System.Database;
using Inventory_Management_System.Models.Db_models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SupplierController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var suppliers = await _context.Suppliers.ToListAsync();
            return Ok(new
            {
                Message = "Suppliers fetched successfully.",
                data = suppliers

            });
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Supplier supplier)
        {
            if (supplier == null)
                return BadRequest("Supplier data is required.");

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = supplier.SupplierId }, new
            {
                Message = "Supplier added successfully",
                data = supplier
            });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
                return NotFound();

            return Ok(new
            {
                Message = "Supplier fetched successfully.",
                data = supplier

            });
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Supplier supplier)
        {
            if (id != supplier.SupplierId)
                return BadRequest();

            _context.Entry(supplier).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "Supplier updated successfully.",
                data = supplier

            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
                return NotFound();

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "Supplier deleted successfully",
                data = supplier
            });
        }
    }
}
