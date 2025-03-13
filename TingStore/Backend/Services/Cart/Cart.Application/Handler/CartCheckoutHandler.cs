using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Application.Commands;
using Cart.Application.Dtos;
using Cart.Application.Mappers;
using Cart.Core.Repositories;
using MediatR;

namespace Cart.Application.Handler
{
    public class CartCheckoutHandler : IRequestHandler<CartCheckoutCommand, CartShoppingDTO>
    {
        private readonly ICartRepository _cartRepository;

        public CartCheckoutHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<CartShoppingDTO> Handle(CartCheckoutCommand request, CancellationToken cancellationToken)
        {
            var model  = await _cartRepository.CheckOut(request.UserId, request.ProductIds);
            return CartMapper.Mapper.Map<CartShoppingDTO>(model);
        }
    }
}
