using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discount.Application.Dtos;
using MediatR;

namespace Discount.Application.Commands
{
    public class CreateCouponCommand : IRequest<CouponDTO>
    {
        public CouponDTO Coupon { get; set; }
        public CreateCouponCommand(CouponDTO coupon)
        {
            Coupon = coupon;
        }
    }
}
