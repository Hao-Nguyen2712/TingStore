using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Order.Application.Commands;
using Order.Core.Repositories;

namespace Order.Application.Handlers
{
    public class UpdateStatusOrderHandler : IRequestHandler<UpdateStatusOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        public UpdateStatusOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Task<bool> Handle(UpdateStatusOrderCommand request, CancellationToken cancellationToken)
        {
            return _orderRepository.UpdateOrderStatus(request.Id, request.Status);
        }
    }
}
