using AutoMapper;
using KeyFactor.Carbone.Configuration.Products;

namespace KeyFactor.Carbone.Configuration
{
    public class ConfigurationApplicationAutoMapperProfile : Profile
    {
        public ConfigurationApplicationAutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
       }
    }
}