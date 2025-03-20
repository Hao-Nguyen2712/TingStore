using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Dtos
{
    public class GetTotalValueResponse
    {
        public string Id { get; set; }
        public decimal TotalValue { get; set; }
    }
}
