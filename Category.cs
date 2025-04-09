using System.Collections.Generic;

namespace Inventory_Management_System.Models.Db_models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
