using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Cart.Application.Commands
{
    public class DeleteProductFromCartCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public List<string> ProductIdToRemove { get; set; } = new List<string>();

        public DeleteProductFromCartCommand() { }
        public DeleteProductFromCartCommand(int userId, List<string> productIdsToRemove)
        {
            UserId = userId;
            ProductIdToRemove = productIdsToRemove;
        }
    }
}
