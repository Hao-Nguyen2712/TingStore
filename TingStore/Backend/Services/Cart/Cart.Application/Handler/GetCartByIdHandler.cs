using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Application.Dtos;
using Cart.Application.Mappers;
using Cart.Application.Queries;
using Cart.Core.Repositories;
using MediatR;

namespace Cart.Application.Handler
{
    public class GetCartByIdHandler : IRequestHandler<GetCartByIdQuery, CartShoppingDTO>
    {
        private readonly ICartRepository _cartRepository;

        public GetCartByIdHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }


        public async Task<CartShoppingDTO> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetCart(request.Id);
            if(cart != null)
            {
                return CartMapper.Mapper.Map<CartShoppingDTO>(cart);
            }
            throw new Exception("Cart not found");
        }
    }
}
