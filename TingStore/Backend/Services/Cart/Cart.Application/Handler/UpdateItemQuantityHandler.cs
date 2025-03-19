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
    public class UpdateItemQuantityHandler : IRequestHandler<UpdateItemsQuantityCommand>
    {
        private readonly ICartRepository _cartRepository;

        public UpdateItemQuantityHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task Handle(UpdateItemsQuantityCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetCart(request.UserId);
            if (cart == null)
            {   
                throw new Exception("Cart not found");
            }
            var cartItem = cart.Items.FirstOrDefault(x => x.ProductId == request.ProductId);
            if (cartItem == null)
            {
                throw new Exception("Product not found in cart");
            }
            if(request.Quantity == 0)
            {
                cart.Items.Remove(cartItem);
                await _cartRepository.UpdateCart(cart);
                return;
            }   
            cartItem.Quantity = request.Quantity;
            await _cartRepository.UpdateCart(cart);
        }
    }
}
