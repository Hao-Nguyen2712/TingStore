using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Payment.Application.Commands;
using Payment.Core.Repositories;

namespace Payment.Application.Handlers
{
    public class UpdateTrasactionHandler : IRequestHandler<UpdateTransactionCommand, string>
    {
        private readonly IPaymentTransactionRepository _paymentTransactionRepository;

        public UpdateTrasactionHandler(IPaymentTransactionRepository paymentTransactionRepository)
        {
            _paymentTransactionRepository = paymentTransactionRepository;
        }
        public async Task<string> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _paymentTransactionRepository.GetByIdAsync(request.Id);
            if (transaction == null)
            {
                return null;
            }
            transaction.TransactionId = request.TransactionId;
            transaction.Status = request.Status;
            await _paymentTransactionRepository.UpdateAsync(transaction);
            return transaction.OrderId.ToString();
        }
    }
}
