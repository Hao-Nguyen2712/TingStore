using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Order.Application.Commands
{
   public class DeleteOrderCommand : IRequest<bool>
    {
        public string OrderId { get; set; } 
        public DeleteOrderCommand(string orderId)
        {
            OrderId = orderId;
        }
    }
}
