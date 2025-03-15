using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Order.Application.Commands
{
    // update order bao gồm voucher của user và giá trị đơn hàng 
    public class UpdateStatusOrderCommand : IRequest<bool>
    {
        public Guid Id { get; set; } // id của order
        public string Status { get; set; }

        public UpdateStatusOrderCommand(Guid id, string status)
        {
            Id = id;
            Status = status;
        }

    }
}
