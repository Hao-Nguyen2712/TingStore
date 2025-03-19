using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNPAY.NET;

namespace Payment.Application.Services
{
   public interface IVnpayService 
    {
        public string CreatePaymentUrl(double totalMoney, string description, string ipDevice);
        
    }
}
