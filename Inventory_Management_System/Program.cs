using Inventory_Management_System.Database;
using Inventory_Management_System.MappingProfiles;
using Inventory_Management_System.Repositories;
using Inventory_Management_System.Repository_Interfaces;
using Inventory_Management_System.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddSingleton<SoftDeleteInterceptor>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();


// Register AppDbContext 

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var serviceProvider = builder.Services.BuildServiceProvider();
    var interceptor = serviceProvider.GetRequiredService<SoftDeleteInterceptor>();
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultCn"))
           .AddInterceptors(interceptor);
});

// Add Controllers

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
