using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Cart.Application.Commands
{
   public class DeleteCartCommand: IRequest<Unit>
    {
        public string Id { get; set; }
        public DeleteCartCommand(string id)
        {
            Id = id;
        }
    }
}
