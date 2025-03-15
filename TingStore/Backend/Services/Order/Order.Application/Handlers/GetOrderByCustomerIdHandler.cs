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
    public class GetOrderByCustomerIdHandler : IRequestHandler<GetOrderByCustomerIdQuerry, List<OrderDTO>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByCustomerIdHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<OrderDTO>> Handle(GetOrderByCustomerIdQuerry request, CancellationToken cancellationToken)
        {
            var result =  await _orderRepository.GetOrdersByCustomerId(request.CustomerId);
            if (result == null)
            {
                throw new ArgumentNullException();
            }
            return OrderMapper.Mapper.Map<List<OrderDTO>>(result);
        }
    }
}
