using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cart.Application.Dtos;
using Cart.Core.Entities;

namespace Cart.Application.Mappers
{
    public class CartMappingProfile : Profile
    {
        public CartMappingProfile() {
            CreateMap<CartShopping, CartShoppingDTO>().ReverseMap();
            CreateMap<CartShoppingItem, CartShoppingItemDTO>().ReverseMap();
        }
    }
}
