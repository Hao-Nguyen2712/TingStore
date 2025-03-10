using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discount.Api.Protos;
using Grpc.Core;
using Order.Application.Services;
using static Discount.Api.Protos.DiscountServicegRPC;

namespace Order.Infrastructure.ExternalServices
{
    public class DiscountClientService : IDiscountClientService
    {
        private readonly DiscountServicegRPCClient _discountServicegRpcClient;

        public DiscountClientService(DiscountServicegRPCClient discountServicegRpcClient)
        {
            _discountServicegRpcClient = discountServicegRpcClient;
        }

        public Task<decimal> GetValue()
        {
            _discountServicegRpcClient.GetCoupon(new GetCouponRequest { CouponName = "test" });
            throw new Exception();
        }
    }
}
