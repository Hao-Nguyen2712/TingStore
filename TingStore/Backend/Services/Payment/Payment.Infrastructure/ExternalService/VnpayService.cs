using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Payment.Application.Services;
using VNPAY.NET;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;

namespace Payment.Infrastructure.ExternalService
{
   public class VnpayService  : IVnpayService
    {
        private readonly IVnpay _vnpay;

        public VnpayService(IVnpay vnpay)
        {
            _vnpay = vnpay;
        }

        public string CreatePaymentUrl(double totalMoney , string description, string ipDevice)
        {
            var paymentRequest = new PaymentRequest
            {
                PaymentId = DateTime.Now.Ticks,
                Money =  totalMoney,
                Description = description,
                IpAddress = ipDevice,
                BankCode = BankCode.ANY,
                Currency = Currency.VND,
                Language = DisplayLanguage.Vietnamese
            };

            var paymentUrl = _vnpay.GetPaymentUrl(paymentRequest);
            return paymentUrl;
        }
    }
}
