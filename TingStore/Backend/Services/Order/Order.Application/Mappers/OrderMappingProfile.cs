using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventBus.Messages.Events;
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

            CreateMap<CartCheckoutEvent, CreateOrderCommand>()
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Amount)) // Ánh xạ Amount thành TotalAmount
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<EventBus.Messages.Events.Item, Application.Commands.Item>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<CreateOrderCommand, OrderDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())) 
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.UtcNow)) 
            .ForMember(dest => dest.UpdateAt, opt => opt.Ignore()) 
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
            .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => (decimal?)null)) 
            .ForMember(dest => dest.FinalAmount, opt => opt.MapFrom(src => src.TotalAmount)) 
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => OrderStatusDTO.Waiting.ToString())) 
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

           
            CreateMap<Application.Commands.Item, OrderItemDTO>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) 
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}
