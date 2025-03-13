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
        public List<string> ProductIdToRemove { get; set; }

        public DeleteProductFromCartCommand(int userid, List<string> productIdsToRemove)
        {
           UserId = userid;
           ProductIdToRemove = productIdsToRemove;
        }
    }
}
