using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Discount.Application.Commands
{
    public class ReturnCouponCommand : IRequest<bool>
    {
        public string Id { get; set; }
    }
}
