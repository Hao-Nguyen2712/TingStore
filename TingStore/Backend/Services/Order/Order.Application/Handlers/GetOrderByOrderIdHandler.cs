using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Order.Application.DTOs;
using Order.Application.Extensions;
using Order.Application.Mappers;
using Order.Application.Queries;
using Order.Core.Repositories;

namespace Order.Application.Handlers
{
   public class GetOrderByOrderIdHandler : IRequestHandler<GetOrderByOrderIdQuerry, OrderDTO>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByOrderIdHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<OrderDTO> Handle(GetOrderByOrderIdQuerry request, CancellationToken cancellationToken)
        {
            var id = Guid.Parse(request.OrderId);
            var result = await _orderRepository.GetOrderById(id);
            if (result == null) {
                throw new OrderNotFoundExtension("Order" , request.OrderId);
            }
            return OrderMapper.Mapper.Map<OrderDTO>(result);
        }
    }
   
}
