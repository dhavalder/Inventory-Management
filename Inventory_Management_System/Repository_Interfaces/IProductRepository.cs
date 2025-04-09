using Inventory_Management_System.Models.Db_models;

namespace Inventory_Management_System.Repository_Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllWithCategoryAsync();
        Task<Product> GetByIdWithCategoryAsync(int id);

        void Update(Product product);
    }
}
