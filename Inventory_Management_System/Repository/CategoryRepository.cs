using Inventory_Management_System.Database;
using Inventory_Management_System.Models.Db_models;
using Microsoft.EntityFrameworkCore;
public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync() =>
        await _context.Categories.ToListAsync();

    public async Task<Category> GetByIdAsync(int id) =>
        await _context.Categories.FindAsync(id);

    public async Task<Category> GetByNameAsync(string name) =>
        await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);

    public async Task AddAsync(Category entity) =>
        await _context.Categories.AddAsync(entity);

    public void Update(Category entity) =>
        _context.Categories.Update(entity);

    public void Delete(Category entity) =>
        _context.Categories.Remove(entity);

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}
