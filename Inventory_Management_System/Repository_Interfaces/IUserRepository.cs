using Inventory_Management_System.Models.Db_models;

namespace Inventory_Management_System.Repository_Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();
    }
}
