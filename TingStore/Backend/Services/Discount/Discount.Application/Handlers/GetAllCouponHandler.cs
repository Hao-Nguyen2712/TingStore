using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discount.Application.Dtos;
using Discount.Application.Mappers;
using Discount.Application.Queries;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers
{
    public class GetAllCouponHandler : IRequestHandler<GetAllCouponQuery, List<CouponDTO>>
    {
        private readonly ICouponRepository _couponRepository;

        public GetAllCouponHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task<List<CouponDTO>> Handle(GetAllCouponQuery request, CancellationToken cancellationToken)
        {
            var result = await  _couponRepository.GetCoupons();
            return DiscountMapper.Mapper.Map<List<CouponDTO>>(result);
        }
    }
}
