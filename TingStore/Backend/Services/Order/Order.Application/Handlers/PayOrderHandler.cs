using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Order.Application.Commands;
using Order.Application.DTOs;
using Order.Core.Repositories;

namespace Order.Application.Handlers
{
    public class PayOrderHandler : IRequestHandler<PayOrderCommand, PayOrderResponse>
    {
        private readonly IOrderRepository _orderRepository;

        public PayOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<PayOrderResponse> Handle(PayOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(request.OrderId);
            if (order == null)
            {
                return null;
            }
            // cập nhật giá trị của trạng thái đơn hàng
            var result = await _orderRepository.UpdateOrderStatus(request.OrderId, OrderStatusDTO.Processing.ToString());
            if (result)
            {
                return new PayOrderResponse
                {
                    OrderId = order.Id,
                    Amount = order.FinalAmount,
                };
            }
            return null;
        }
    }
}
