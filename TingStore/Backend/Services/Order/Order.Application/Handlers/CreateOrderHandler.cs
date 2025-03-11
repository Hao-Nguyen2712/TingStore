using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Order.Application.Commands;
using Order.Application.DTOs;
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
            if (request == null)
            {
                throw new ArgumentNullException();
            }
            decimal couponValue = 0;

            if (!string.IsNullOrEmpty(request.Code))
            {
                var couponResponse = await _discountClientService.GetValue(request.Code, request.OrderDTO.TotalAmount);
                couponValue = couponResponse.Value;
            }
            request.OrderDTO.DiscountAmount = couponValue;
            var orderModel = OrderMapper.Mapper.Map<Core.Entities.Order>(request.OrderDTO);
            var result = await _orderRepository.AddOrder(orderModel);
            if (result == null)
            {
                throw new InvalidOperationException("Can't add Order.");
            }
            return OrderMapper.Mapper.Map<OrderDTO>(result);
        }
    }
}
