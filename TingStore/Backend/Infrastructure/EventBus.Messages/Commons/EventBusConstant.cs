using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Commons
{
    public static class EventBusConstant
    {
        public const string CartCheckoutQueue = "cartcheckout-queue";
        public const string PaymentRequestQueue = "paymentrequest-queue";
        public const string UpdatePaymentStatusQueue = "updatepaymentstatus-queue";
        public const string UpadateDiscountQuantityQueue = "updatediscountquantity-queue";
    }
}
