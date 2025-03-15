using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Order.Application.Commands;
using Order.Application.DTOs;
using Order.Application.Extensions;
using Order.Application.Mappers;
using Order.Application.Services;
using Order.Core.Repositories;

namespace Order.Application.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderDTO>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDiscountClientService _discountClientService;
        public CreateOrderHandler(IOrderRepository orderRepository, IDiscountClientService discountClientService)
        {
            _orderRepository = orderRepository;
            _discountClientService = discountClientService;
        }
        public async Task<OrderDTO> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // khởi tạo giá trị cho coupon
            decimal couponValue = 0;

            if (!string.IsNullOrEmpty(request.Code))
            {
                // check the coupon value
                var couponResponse = await _discountClientService.GetValue(request.Code, request.TotalAmount);
                couponValue = couponResponse.Value;
            }

            var orderDTO = OrderMapper.Mapper.Map<OrderDTO>(request);
            orderDTO.DiscountAmount = couponValue;
            var orderModel = OrderMapper.Mapper.Map<Core.Entities.Order>(orderDTO);

            if (orderModel == null)
            {
                throw new MapperErrorExtension(orderDTO, typeof(Core.Entities.Order));
            }

            var result = await _orderRepository.AddOrder(orderModel);
            if (result == null)
            {
                throw new InvalidOperationException("Failed to create order");
            }
            return OrderMapper.Mapper.Map<OrderDTO>(result);
        }
    }
}
