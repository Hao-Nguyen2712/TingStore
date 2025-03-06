using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discount.Application.Dtos;
using MediatR;

namespace Discount.Application.Queries
{
   public class GetCouponByNameQuery : IRequest<CouponDTO>
    {
        public string CouponName { get; set; }
        public GetCouponByNameQuery(string couponName)
        {
            CouponName = couponName;
        }
    }
}
