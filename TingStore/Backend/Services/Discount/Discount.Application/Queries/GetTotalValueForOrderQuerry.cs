using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Discount.Application.Queries
{
    public class GetTotalValueForOrderQuerry : IRequest<decimal>
    {
        public string Code { get; set; }
        public decimal TotalValue { get; set; }

        public GetTotalValueForOrderQuerry(string code, decimal totalValue)
        {
            Code = code;
            TotalValue = totalValue;
        }
    }
}
