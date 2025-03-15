using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Order.Application.Commands
{
   public class DeleteOderItemCommand : IRequest<bool>
    {
        public int Id { get; set; } // id của order
        public DeleteOderItemCommand(int id)
        {
            Id = id;
        }
    }
}
