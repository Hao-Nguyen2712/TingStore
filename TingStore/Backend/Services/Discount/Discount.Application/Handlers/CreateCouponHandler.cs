using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discount.Application.Commands;
using Discount.Application.Dtos;
using Discount.Application.Mappers;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers
{
    public class CreateCouponHandler : IRequestHandler<CreateCouponCommand, CouponDTO>
    {
        private readonly ICouponRepository _couponRepository;

        public CreateCouponHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task<CouponDTO> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = DiscountMapper.Mapper.Map<Coupon>(request.Coupon)
                ?? throw new Exception("Error mapper coupon");
            var result = await _couponRepository.AddCoupon(coupon);
            return DiscountMapper.Mapper.Map<CouponDTO>(result);
        }
    }
}
