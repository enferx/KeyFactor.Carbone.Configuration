using AutoMapper;
using KeyFactor.Carbone.Configuration.Products;
using KeyFactor.Carbone.Configuration.Units;

namespace KeyFactor.Carbone.Configuration
{
    public class ConfigurationApplicationAutoMapperProfile : Profile
    {
        public ConfigurationApplicationAutoMapperProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateUpdateProductDto, Product>();
            CreateMap<Unit, UnitDto>();
            CreateMap<CreateUpdateUnitDto, Unit>();
            CreateMap<ProductProperty, ProductPropertyDto>();
       }
    }
}