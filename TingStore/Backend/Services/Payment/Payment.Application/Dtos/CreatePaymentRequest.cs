using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.Dtos
{
   public class CreatePaymentRequest
    {
        public Guid OrderID { get; set; }
        public decimal Amount { get; set; }
    }
}
