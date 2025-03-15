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
    public class DeleteOrderItemHandler : IRequestHandler<DeleteOderItemCommand, bool>
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public DeleteOrderItemHandler(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }
        public Task<bool> Handle(DeleteOderItemCommand request, CancellationToken cancellationToken)
        {
            return _orderItemRepository.DeleteOrderItem(request.Id);
        }
    }
}
