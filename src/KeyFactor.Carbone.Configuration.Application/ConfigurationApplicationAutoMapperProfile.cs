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
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Unit, UnitDto>();
            CreateMap<CreateUnitDto, Unit>();
            CreateMap<UpdateUnitDto, Unit>();
       }
    }
}