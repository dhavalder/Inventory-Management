using Inventory_Management_System.Models.Dto;
using Inventory_Management_System.Services;
using Microsoft.AspNetCore.Mvc;


namespace Inventory_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(new
            {
                Message = "Products fetched successfully.",
                data = products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning("Product with Id {id} not found", id);
                return NotFound();
            }

            return Ok(new
            {
                Message = "Product fetched successfully.",
                data = product
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            if (dto == null)
                return BadRequest("Product data is required.");

            try
            {
                var createdProduct = await _productService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, new

                {

                    Message = "Product added successfully",
                    data = createdProduct

                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            if (dto == null || id != dto.Id)
                return BadRequest("Invalid product update request.");

            var existing = await _productService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Product with ID {id} does not exist.");

            var updatedProduct = await _productService.UpdateAsync(dto);
            return Ok(new
            {
                Message = "Product updated successfully",
                data = updatedProduct
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteAsync(id);
            if (!result)
                return NotFound($"Product with ID {id} not found.");

            return Ok(new
            {
                Message = "Product deleted successfully"
            });

        }
    }
}



// dotnet inventory_Management_System.dll
//info: Microsoft.Hosting.Lifetime[14]
//      Now listening on: http://localhost:5000
//info: Microsoft.Hosting.Lifetime[0]
//      Application started. Press Ctrl+C to shut down.
//info: Microsoft.Hosting.Lifetime[0]
//      Hosting environment: Production
//info: Microsoft.Hosting.Lifetime[0]
//      Content root path: D:\Inventory Management\Inventory Management\publish


