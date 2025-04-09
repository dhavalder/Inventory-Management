using AutoMapper;
using Inventory_Management_System.Models.Db_models;
using Inventory_Management_System.Models.Dto;


namespace Inventory_Management_System.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>()
     .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();


        }
    }
}
