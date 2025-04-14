using System.Text.Json.Serialization;

namespace Inventory_Management_System.Models.Db_models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
