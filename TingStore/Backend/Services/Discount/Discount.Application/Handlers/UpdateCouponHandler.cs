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
    public class UpdateCouponHandler : IRequestHandler<UpdateCouponCommand, bool>
    {
        private readonly ICouponRepository _couponRepository;

        public UpdateCouponHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }
        public async Task<bool> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = DiscountMapper.Mapper.Map<Coupon>(request.couponDTO)
               ?? throw new Exception("Error Mapper Discount");
            var result = await _couponRepository.UpdateCoupon(coupon);
            if (!result)
            {
                return false;
            }
            return true;
        }
    }

}
