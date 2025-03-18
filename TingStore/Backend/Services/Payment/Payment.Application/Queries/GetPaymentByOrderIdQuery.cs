using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Payment.Application.Dtos;

namespace Payment.Application.Querries
{
    public class GetPaymentByOrderIdQuery : IRequest<PaymentTransactionDTO>
    {
        public Guid OrderId { get; set; }
        public GetPaymentByOrderIdQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
