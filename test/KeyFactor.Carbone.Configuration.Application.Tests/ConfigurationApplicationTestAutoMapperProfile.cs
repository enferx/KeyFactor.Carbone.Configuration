using AutoMapper;
using KeyFactor.Carbone.Configuration.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyFactor.Carbone.Configuration
{
    public class ConfigurationApplicationTestAutoMapperProfile : Profile
    {
        public ConfigurationApplicationTestAutoMapperProfile()
        {
            CreateMap<ProductDto, UpdateProductDto>();
        }

    }
}
