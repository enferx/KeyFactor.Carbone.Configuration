using AutoMapper;
using KeyFactor.Carbone.Configuration.Products;

namespace KeyFactor.Carbone.Configuration.Web
{
    public class ConfigurationWebAutoMapperProfile : Profile
    {
        public ConfigurationWebAutoMapperProfile()
        {
            CreateMap<ProductDto, CreateProductDto>();
            CreateMap<ProductDto, UpdateProductDto>();

        }
    }
}