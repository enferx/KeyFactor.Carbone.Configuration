using AutoMapper;
using KeyFactor.Carbone.Configuration.Products;
using KeyFactor.Carbone.Configuration.Units;

namespace KeyFactor.Carbone.Configuration.Web
{
    public class ConfigurationWebAutoMapperProfile : Profile
    {
        public ConfigurationWebAutoMapperProfile()
        {
            CreateMap<ProductDto, CreateUpdateProductDto>();
            CreateMap<UnitDto, CreateUpdateUnitDto>();
            CreateMap<ProductPropertyDto, CreateUpdateProductPropertyDto>();
        }
    }
}