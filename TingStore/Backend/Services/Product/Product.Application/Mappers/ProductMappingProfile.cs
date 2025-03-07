using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using AutoMapper;
using Product.Application.Responses;
using Product.Core.Specs;
using Product.Core.Models;
using Product.Application.Commands;

namespace Product.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product.Core.Models.Product, ProductResponse>().ReverseMap();
            CreateMap<Product.Core.Models.Product, CreateProductCommand>().ReverseMap();
            CreateMap<Pagination<Product.Core.Models.Product>, Pagination<ProductResponse>>().ReverseMap();
        }
    }
}
