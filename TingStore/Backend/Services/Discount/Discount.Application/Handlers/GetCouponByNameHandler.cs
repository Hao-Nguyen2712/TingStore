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
    public class GetCouponByNameHandler : IRequestHandler<GetCouponByNameQuery, CouponDTO>
    {
        private readonly ICouponRepository _couponRepository;
        public GetCouponByNameHandler(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task<CouponDTO> Handle(GetCouponByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await _couponRepository.GetCouponByName(request.CouponName);
            return DiscountMapper.Mapper.Map<CouponDTO>(result);
        }
    }

}
