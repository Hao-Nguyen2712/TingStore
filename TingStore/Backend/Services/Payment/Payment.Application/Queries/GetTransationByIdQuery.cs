using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Payment.Application.Dtos;

namespace Payment.Application.Querries
{
   public class GetTransationByIdQuery : IRequest<PaymentTransactionDTO>
    {
        public Guid Id { get; set; }
        public GetTransationByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
