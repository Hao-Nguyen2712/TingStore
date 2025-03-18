using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Application.Extension
{
    public class PaymentNullException : Exception
    {
        public  PaymentNullException(object model) : base($"{model} is not find")
        {
        }
    }
}
