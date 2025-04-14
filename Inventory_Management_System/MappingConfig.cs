using AutoMapper;
using Inventory_Management_System.Models.Db_models;
using Inventory_Management_System.Models.Dto;

namespace Inventory_Management_System.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<CreateProductDto, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore());

            CreateMap<UpdateProductDto, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore());


            CreateMap<Product, ProductDto>();
        }
    }
}
