namespace Inventory_Management_System.Repository_Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}