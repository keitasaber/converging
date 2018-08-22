using AutoMapper;
using Converging.Model.Models;
using Converging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Converging.Mappings
{
    public class AutoMapperConfiguration
    {
        public static AutoMapper.IMapper Mapping;
        public static void RegisterAutoMappers()
        {
            Mapping = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductViewModel>();
                config.CreateMap<ProductCategory, ProductCategoryViewModel>();
                config.CreateMap<Tag, TagViewModel>();
                config.CreateMap<ProductTag, ProductTagViewModel>();
                config.CreateMap<Footer, FooterViewModel>();
            }).CreateMapper();
        }
    }
}