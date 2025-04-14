namespace Inventory_Management_System.Models.Dto
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string SubCategory { get; set; }
        public string? Sku { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
