using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discount.Application.Dtos;
using MediatR;

namespace Discount.Application.Queries
{
    public class GetTotalValueForOrderQuerry : IRequest<GetTotalValueResponse>
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
