using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Order.Application.Commands
{
    public class PayOrderCommand : IRequest<PayOrderResponse>
    {
        public Guid OrderId { get; set; }
    }


    // lớp trả về giá trị
    public class PayOrderResponse
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
    }
}
