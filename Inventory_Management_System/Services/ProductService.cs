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
        var product = _mapper.Map<Product>(dto);


        if (string.IsNullOrWhiteSpace(product.Sku))
        {
            product.Sku = GenerateSku(dto.Name, dto.SubCategory);
        }

        var created = await _productRepo.CreateAsync(product);
        return _mapper.Map<ProductDto>(created);
    }


    private string GenerateSku(string name, string subCategory)
    {

        var shortName = new string(name.Where(char.IsLetterOrDigit).ToArray()).ToUpper().Substring(0, Math.Min(3, name.Length));
        var shortSubCat = new string(subCategory.Where(char.IsLetterOrDigit).ToArray()).ToUpper().Substring(0, Math.Min(3, subCategory.Length));
        var randomCode = Guid.NewGuid().ToString().Substring(0, 4).ToUpper();

        return $"{shortName}-{shortSubCat}-{randomCode}";
    }



    public async Task<ProductDto> UpdateAsync(UpdateProductDto dto)
    {
        var existing = await _productRepo.GetByIdAsync(dto.Id);
        if (existing == null)
            throw new ArgumentException($"Product with ID {dto.Id} does not exist.");


        Console.WriteLine($"Before update: Quantity = {existing.Quantity}");
        _mapper.Map(dto, existing);
        Console.WriteLine($"After update: Quantity = {existing.Quantity}");


        if (string.IsNullOrWhiteSpace(existing.Sku))
        {
            existing.Sku = GenerateSku(existing.Name, existing.SubCategory);
        }

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
