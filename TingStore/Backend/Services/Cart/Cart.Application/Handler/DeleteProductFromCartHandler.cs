using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Application.Commands;
using Cart.Core.Repositories;
using MediatR;

namespace Cart.Application.Handler
{
    public class DeleteProductFromCartHandler : IRequestHandler<DeleteProductFromCartCommand, bool>
    {
        private readonly ICartRepository _cartRepository;

        public DeleteProductFromCartHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public Task<bool> Handle(DeleteProductFromCartCommand request, CancellationToken cancellationToken)
        {
            return _cartRepository.RemoveProductFromCart(request.UserId, request.ProductIdToRemove);
        }
    }
}
