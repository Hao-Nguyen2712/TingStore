using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Application.Dtos;
using MediatR;

namespace Cart.Application.Commands
{
    public class CartCheckoutCommand : IRequest<CartShoppingDTO>
    {
        public int UserId { get; set; }
        public List<string> ProductIds { get; set; }
    }
}
