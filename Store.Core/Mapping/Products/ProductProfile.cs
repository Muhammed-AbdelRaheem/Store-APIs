using AutoMapper;
using Store.Core.Dtos;
using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Mapping.Products
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(P=>P.BrandName ,options=>options.MapFrom(s=>s.Brand.Name))
                .ForMember(P=>P.TypeName ,options=>options.MapFrom(s=>s.Type.Name));

            CreateMap<ProductBrand, TypeBrandDto>();
            CreateMap<ProductType, TypeBrandDto>();


        }
    }
}
