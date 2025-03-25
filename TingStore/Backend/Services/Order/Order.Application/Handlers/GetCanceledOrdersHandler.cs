using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Order.Application.DTOs;
using Order.Application.Mappers;
using Order.Application.Queries;
using Order.Core.Repositories;

namespace Order.Application.Handlers
{
    public class GetCanceledOrdersHandler : IRequestHandler<GetCanceledOrdersQuery, List<OrderDTO>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetCanceledOrdersHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<OrderDTO>> Handle(GetCanceledOrdersQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetCanceledOrders();
            if(order == null)
            {
                throw new ArgumentNullException();
            }
            return OrderMapper.Mapper.Map<List<OrderDTO>>(order);
        }
    }
}
