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
    public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, Unit>
    {
        private readonly ICartRepository _cartRepository;

        public DeleteCartHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<Unit> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            await _cartRepository.DeleteCart(request.Id);
            return Unit.Value;
        }
    }
}
