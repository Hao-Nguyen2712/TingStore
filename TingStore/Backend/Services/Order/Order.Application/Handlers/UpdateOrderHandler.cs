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
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, OrderDTO>
    {
        private readonly IOrderRepository _orderRepository;
        private IDiscountClientService _discountClientService;
        private readonly IOrderItemRepository _orderItemRepository;
        public UpdateOrderHandler(IOrderRepository orderRepository, IDiscountClientService discountClientService, IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _discountClientService = discountClientService;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<OrderDTO> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var updateOrder = OrderMapper.Mapper.Map<OrderDTO>(await _orderRepository.GetOrderById(request.Id));
            if (updateOrder == null)
            {
                throw new KeyNotFoundException("Order not found");
            }
            string discoutId = "";
            if (!string.IsNullOrEmpty(request.Code))
            {
                var couponResponse = await _discountClientService.GetValue(request.Code, request.TotalAmount);
                updateOrder.DiscountAmount = couponResponse.Value;
                discoutId = couponResponse.Id ?? string.Empty;
            }
          
            updateOrder.FinalAmount = updateOrder.TotalAmount - updateOrder.DiscountAmount ?? 0;
            updateOrder.DiscountId = discoutId;

            if (request.Item?.Any() == true)
            {
                foreach (var itemUpdate in request.Item)
                {
                    var orderItem = updateOrder.Items.FirstOrDefault(x => x.Id == itemUpdate.ItemId);
                    if (orderItem != null)
                    {
                        if (itemUpdate.IsDelete)
                        {
                            await _orderItemRepository.DeleteOrderItem(itemUpdate.ItemId);
                        }
                        else if (itemUpdate.Quantity.HasValue && itemUpdate.Quantity > 0)
                        {
                            await _orderItemRepository.UpdateOrderItemQuantity(itemUpdate.ItemId, itemUpdate.Quantity.Value);
                        }
                    }
                }
            }

            var result = await _orderRepository.UpdateOrder(OrderMapper.Mapper.Map<Core.Entities.Order>(updateOrder));
            if (result == null)
            {
                throw new Exception("Failed to update order");
            }
            return OrderMapper.Mapper.Map<OrderDTO>(result);
        }
    }
}
