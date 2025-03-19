using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Payment.Application.Dtos;
using Payment.Application.Extension;
using Payment.Application.Mappers;
using Payment.Application.Querries;
using Payment.Core.Repositories;

namespace Payment.Application.Handlers
{
    public class GetPaymentByOrderIdHandler : IRequestHandler<GetPaymentByOrderIdQuery, PaymentTransactionDTO>
    {
        private readonly IPaymentTransactionRepository _paymentTransactionRepository;

        public GetPaymentByOrderIdHandler(IPaymentTransactionRepository paymentTransactionRepository)
        {
            _paymentTransactionRepository = paymentTransactionRepository;
        }
        public async Task<PaymentTransactionDTO> Handle(GetPaymentByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var model =  await _paymentTransactionRepository.GetByOrderIdAsync(request.OrderId);
            if (model == null)
            {
                throw new PaymentNullException(typeof(Core.Entities.PaymentTransaction));
            }
            return PaymentMapper.Mapper.Map<PaymentTransactionDTO>(model);
        }
    }
}
