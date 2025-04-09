using Inventory_Management_System.Models.Db_models;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> GetByIdAsync(int id);
    Task<Category> GetByNameAsync(string name);
    Task AddAsync(Category entity);
    void Update(Category entity);
    void Delete(Category entity);
    Task SaveChangesAsync();
}
