using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discount.Application.Dtos;
using MediatR;

namespace Discount.Application.Queries
{
    public class GetAllCouponQuery : IRequest<List<CouponDTO>>
    {
    }
}
