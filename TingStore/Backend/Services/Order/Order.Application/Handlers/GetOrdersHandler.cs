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
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuerry, List<OrderDTO>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<OrderDTO>> Handle(GetOrdersQuerry request, CancellationToken cancellationToken)
        {
            var result = await _orderRepository.GetOrders();
            if (result == null)
            {
                throw new ArgumentNullException();
            }
            return OrderMapper.Mapper.Map<List<OrderDTO>>(result);
        }
    }
}
