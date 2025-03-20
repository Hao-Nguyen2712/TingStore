using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Payment.Application.Commands;
using Payment.Application.Dtos;
using Payment.Core.Repositories;

namespace Payment.Application.Handlers
{
    public class AddTransactionHandler : IRequestHandler<AddTransactionCommand, PaymentTransactionDTO>
    {

        private readonly IPaymentTransactionRepository _paymentTransactionRepository;

        public AddTransactionHandler(IPaymentTransactionRepository paymentTransactionRepository)
        {
            _paymentTransactionRepository = paymentTransactionRepository;
        }
        public Task<PaymentTransactionDTO> Handle(AddTransactionCommand request, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}
