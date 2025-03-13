using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cart.Application.Dtos;
using Cart.Core.Entities;
using EventBus.Messages.Events;

namespace Cart.Application.Mappers
{
    public class CartMappingProfile : Profile
    {
        public CartMappingProfile()
        {
            CreateMap<CartShopping, CartShoppingDTO>().ReverseMap();
            CreateMap<CartShoppingItem, CartShoppingItemDTO>().ReverseMap();
            CreateMap<CartShoppingDTO, CartCheckoutEvent>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id)) // Giả sử CustomerId lấy từ Id
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            // Mapping từ CartShoppingItemDTO sang CartItemCheckoutEvent
            CreateMap<CartShoppingItemDTO, CartItemCheckoutEvent>()
                .ConstructUsing(src => new CartItemCheckoutEvent(src.ProductId, src.ProductName, src.Quantity, src.Price));

        }
    }
}
