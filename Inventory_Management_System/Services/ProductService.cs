using AutoMapper;
using Inventory_Management_System.Models.Db_models;
using Inventory_Management_System.Models.Dto;
using Inventory_Management_System.Repository_Interfaces;
using Inventory_Management_System.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepo, ICategoryRepository categoryRepo, IMapper mapper)
    {
        _productRepo = productRepo;
        _categoryRepo = categoryRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _productRepo.GetAllWithCategoryAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        var product = await _productRepo.GetByIdWithCategoryAsync(id);
        if (product == null)
            throw new KeyNotFoundException($"Product with ID {id} not found.");

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto dto)
    {
        var category = await _categoryRepo.GetByIdAsync(dto.CategoryId);
        if (category == null)
            throw new ArgumentException($"Category with ID {dto.CategoryId} does not exist.");

        var product = _mapper.Map<Product>(dto);
        await _productRepo.CreateAsync(product);
        await _productRepo.SaveChangesAsync();

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> UpdateAsync(UpdateProductDto dto)
    {
        var existing = await _productRepo.GetByIdAsync(dto.Id);
        if (existing == null)
            throw new ArgumentException($"Product with ID {dto.Id} does not exist.");

        _mapper.Map(dto, existing);
        _productRepo.Update(existing);
        await _productRepo.SaveChangesAsync();

        return _mapper.Map<ProductDto>(existing);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);
        if (product == null) return false;

        product.IsDeleted = true;
        product.DeletedOnUtc = DateTime.UtcNow;

        _productRepo.Update(product);
        await _productRepo.SaveChangesAsync();
        return true;
    }
}
