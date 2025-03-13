using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Order.Application.Commands;
using Order.Application.DTOs;

namespace Order.Application.Mappers
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Core.Entities.Order, OrderDTO>().ForMember(dest => dest.Items , opt => opt.MapFrom(src => src.OrderItems)).ReverseMap();
            CreateMap<Core.Entities.OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<CreateOrderCommand, Core.Entities.Order>().ReverseMap();
        }
    }
}
