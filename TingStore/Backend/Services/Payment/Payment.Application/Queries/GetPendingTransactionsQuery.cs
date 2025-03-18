using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Payment.Application.Dtos;

namespace Payment.Application.Queries
{
    public record GetPendingTransactionsQuery : IRequest<List<PaymentTransactionDTO>>
    {
    }
}
