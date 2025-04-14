namespace Inventory_Management_System.Models.Dto
{
    public class UpdateProductDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string subCategory { get; set; }
        public string? sku { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }


    }
}
