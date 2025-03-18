using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Payment.Application.Dtos;
using Payment.Application.Extension;
using Payment.Application.Mappers;
using Payment.Application.Queries;
using Payment.Core.Repositories;

namespace Payment.Application.Handlers
{
    public class GetPendingTransactionsHandler : IRequestHandler<GetPendingTransactionsQuery, List<PaymentTransactionDTO>>
    {
        private readonly IPaymentTransactionRepository _paymentTransactionRepository;

        public GetPendingTransactionsHandler(IPaymentTransactionRepository paymentTransactionRepository)
        {
            _paymentTransactionRepository = paymentTransactionRepository;
        }

        public async Task<List<PaymentTransactionDTO>> Handle(GetPendingTransactionsQuery request, CancellationToken cancellationToken)
        {
            var model = await _paymentTransactionRepository.GetPendingTransactionsAsync();
            if(model == null)
            {
                throw new PaymentNullException(typeof(Core.Entities.PaymentTransaction).Name);
            }
            return PaymentMapper.Mapper.Map<List<PaymentTransactionDTO>>(model);
        }
    }
}
