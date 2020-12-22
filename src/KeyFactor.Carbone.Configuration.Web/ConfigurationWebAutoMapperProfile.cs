﻿using AutoMapper;
using KeyFactor.Carbone.Configuration.Products;
using KeyFactor.Carbone.Configuration.Units;

namespace KeyFactor.Carbone.Configuration.Web
{
    public class ConfigurationWebAutoMapperProfile : Profile
    {
        public ConfigurationWebAutoMapperProfile()
        {
            CreateMap<ProductDto, CreateProductDto>();
            CreateMap<ProductDto, UpdateProductDto>();
            CreateMap<UnitDto, CreateUnitDto>();
            CreateMap<UnitDto, UpdateUnitDto>();
        }
    }
}