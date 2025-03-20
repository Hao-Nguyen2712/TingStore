using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Cart.Application.Commands
{
   public class UpdateItemsQuantityCommand : IRequest
    {
        public int UserId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }

        public UpdateItemsQuantityCommand(int userId, string productId, int quantity)
        {
            UserId = userId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
