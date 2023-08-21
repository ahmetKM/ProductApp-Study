using AutoMapper;
using ProductApp.Core.DTOs;
using ProductApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductDto>().ReverseMap(); ;
            CreateMap<Product, ProductUpdateDto>().ReverseMap(); ;
            CreateMap<Category, CategoryDto>().ReverseMap(); ;
            CreateMap<Category, CategoryUpdateDto>().ReverseMap(); ;
            CreateMap<Product, ProductWithCategoryDto>().ReverseMap(); ;
            CreateMap<Category, CategoryWithProducstDto>().ReverseMap(); ;
        }
    }
}
