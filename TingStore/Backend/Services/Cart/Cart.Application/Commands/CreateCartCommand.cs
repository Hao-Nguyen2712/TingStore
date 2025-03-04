using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Application.Dtos;
using MediatR;

namespace Cart.Application.Commands
{
   public class CreateCartCommand : IRequest<CartShoppingDTO>
    {
        public CartShoppingDTO cartShoppingDTO { get; set; }

        public CreateCartCommand(CartShoppingDTO cartShoppingDTO)
        {
            this.cartShoppingDTO = cartShoppingDTO;
        }
    }
}
