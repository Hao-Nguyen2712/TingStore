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
    public class GetTransationByIdHandler : IRequestHandler<GetTransationByIdQuery, PaymentTransactionDTO>
    {
        private readonly IPaymentTransactionRepository _paymentTransactionRepository;

        public GetTransationByIdHandler(IPaymentTransactionRepository paymentTransactionRepository)
        {
            _paymentTransactionRepository = paymentTransactionRepository;
        }

        public async Task<PaymentTransactionDTO> Handle(GetTransationByIdQuery request, CancellationToken cancellationToken)
        {
            var model = await _paymentTransactionRepository.GetByIdAsync(request.Id);
            if(model == null)
            {
                throw new PaymentNullException(typeof(Core.Entities.PaymentTransaction));
            }
            return PaymentMapper.Mapper.Map<PaymentTransactionDTO>(model);  
        }
    }
}
