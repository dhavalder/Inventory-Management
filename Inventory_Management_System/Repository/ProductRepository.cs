using Inventory_Management_System.Database;
using Inventory_Management_System.Models.Db_models;
using Inventory_Management_System.Repository_Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _context.Products.Where(p => !p.IsDeleted).ToListAsync();

        public async Task<IEnumerable<Product>> GetAllWithCategoryAsync() =>
            await _context.Products.Include(p => p.Category).Where(p => !p.IsDeleted).ToListAsync();

        public async Task<Product> GetByIdAsync(int id) =>
            await _context.Products.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

        public async Task<Product> GetByIdWithCategoryAsync(int id) =>
            await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

        public async Task<Product> CreateAsync(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public void UpdateAsync(Product entity)
        {
            _context.Products.Update(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return false;


            product.IsDeleted = true;
            product.DeletedOnUtc = DateTime.UtcNow;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Products.CountAsync();
        }
    }
}
