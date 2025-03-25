using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Payment.Application.Commands;
using Payment.Application.Dtos;
using Payment.Application.Mappers;
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
        public async Task<PaymentTransactionDTO> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {
            request.Status = PaymentStatusDTO.Waiting.ToString();
            request.CreatedAt = DateTime.Now;
            request.PaymentMethod = "VNPAY";
            var model = PaymentMapper.Mapper.Map<Core.Entities.PaymentTransaction>(request);
            var result = await _paymentTransactionRepository.AddAsync(model);
            if (result != null)
            {

                return PaymentMapper.Mapper.Map<PaymentTransactionDTO>(result);
            }
            else
            {
                return null;
            }
        }
    }
}
