using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discount.Application.Dtos;
using MediatR;

namespace Discount.Application.Commands
{
    public class DeleteCouponCommand : IRequest<Unit>
    {
        public string CouponName { get; set; }
        public DeleteCouponCommand(string couponName)
        {
            CouponName = couponName;
        }
    }
}
