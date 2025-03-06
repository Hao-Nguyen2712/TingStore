using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discount.Application.Dtos;
using MediatR;

namespace Discount.Application.Commands
{
    public class UpdateCouponCommand : IRequest<bool>
    {
        public CouponDTO couponDTO { get; set; }
        public UpdateCouponCommand(CouponDTO couponDTO)
        {
            this.couponDTO = couponDTO;
        }
    }
}
