using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using Discount.Application.Dtos;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers
{
    public class GetTotalValueForOrderHandler : IRequestHandler<GetTotalValueForOrderQuerry, GetTotalValueResponse>
    {
        private readonly ICouponRepository _couponRepository;

        public GetTotalValueForOrderHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }
        public async Task<GetTotalValueResponse> Handle(GetTotalValueForOrderQuerry request, CancellationToken cancellationToken)
        {
            var coupon = await _couponRepository.GetCouponByCode(request.Code);
            if (coupon == null || !coupon.IsActive)
            {
                return null;
            }

            if (coupon.MinimumAmount.HasValue && request.TotalValue < coupon.MinimumAmount.Value)
            {
                return null;
            }
            // cập nhật coupon này đã được gán cho order
            coupon.IsReversed = true;
            coupon.ReversedTime = DateTime.Now;
            coupon.UsedCount++;
            coupon.Quantity -= 1;
            await _couponRepository.UpdateCoupon(coupon);

            var result = request.TotalValue - (request.TotalValue * (coupon.Value / 100)); // 10 /1000
            return new GetTotalValueResponse
            {
                Id = coupon.Id.ToString(),
                TotalValue = result
            };
        }
    }
}

