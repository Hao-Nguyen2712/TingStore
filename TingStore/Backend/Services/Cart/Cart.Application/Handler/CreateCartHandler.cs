using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Application.Commands;
using Cart.Application.Dtos;
using Cart.Application.Mappers;
using Cart.Core.Entities;
using Cart.Core.Repositories;
using MediatR;

namespace Cart.Application.Handler
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, CartShoppingDTO>
    {
        private readonly ICartRepository _cartRepository;

        public CreateCartHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartShoppingDTO> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            if (request.cartShoppingDTO == null)
            {
                throw new ArgumentNullException(nameof(request.cartShoppingDTO) , "Request Null");
            }

            var cart = CartMapper.Mapper.Map<CartShopping>(request.cartShoppingDTO);
            if (cart == null)
            {
                throw new InvalidOperationException("Mapper Error");
               
            }        
          
            var result =  await _cartRepository.UpdateCart(cart);
             
            var response = CartMapper.Mapper.Map<CartShoppingDTO>(result);
            if (response == null)
            {
                throw new InvalidOperationException("Mapper Error");
            }
            return response;

        }
    }
}
