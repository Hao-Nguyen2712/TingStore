using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discount.Application.Commands;
using Discount.Application.Mappers;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers
{
    public class DeleteCouponHandler : IRequestHandler<DeleteCouponCommand, Unit>
    {
        private readonly ICouponRepository _couponRepository;

        public DeleteCouponHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }
        public async Task<Unit> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            await _couponRepository.DeleteCoupon(request.CouponName);
            return Unit.Value;
        }
    }
}
