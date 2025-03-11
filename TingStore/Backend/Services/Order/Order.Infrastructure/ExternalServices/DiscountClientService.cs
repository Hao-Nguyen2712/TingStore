using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discount.Api.Protos;
using Grpc.Core;
using Order.Application.DTOs;
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

        public async Task<ValueCouponDTO> GetValue(string code, decimal total)
        {
            var result = await _discountServicegRpcClient.ApplyCouponVoucherAsync(new ApplyCouponVoucherRequest
            {
                CouponCode = code,
                Amount = (double)total
            });
            return new ValueCouponDTO
            {
                Value = (decimal)result.ValueDiscount,
                IsSuccess = result.IsSuccess,
                ErrorMessage = result.Message
            };
        }
    }
}
