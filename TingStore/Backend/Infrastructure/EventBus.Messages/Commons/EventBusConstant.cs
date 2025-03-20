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
      //  public const string CreatePaymentRequestQueue = "createpaymentrequest-queue";
        public const string UpdateOrderStatusQueue = "updateordertstatus-queue";
        public const string UpdateDiscountQuantityQueue = "updatediscountquantity-queue";

    }
}
