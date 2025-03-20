using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discount.Application.Commands;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers
{
    public class ReturnCouponHandler : IRequestHandler<ReturnCouponCommand, bool>
    {
        private readonly ICouponRepository _couponRepository;

        public ReturnCouponHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task<bool> Handle(ReturnCouponCommand request, CancellationToken cancellationToken)
        {
            var id = Guid.Parse(request.Id);
            var coupon = await _couponRepository.GetCouponById(id);
            if (coupon == null)
            {
                throw new ArgumentNullException("Coupon not found");
            }
            // hoàn lại coupon
            coupon.Quantity += 1;
            coupon.UsedCount -= 1;
            return await _couponRepository.UpdateCoupon(coupon);
        }
    }
}
