
using Discount.Api.Helper;
using Discount.Api.Protos;
using Discount.Application.Commands;
using Discount.Application.Dtos;
using Discount.Application.Queries;
using Grpc.Core;
using MediatR;
using static Discount.Api.Protos.DiscountServicegRPC;


namespace Discount.Api.Services
{
    public class DiscountService : DiscountServicegRPCBase
    {
        private readonly IMediator _mediator;
        public DiscountService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<CouponModel> CreateCoupon(CouponModel couponModel, ServerCallContext context)
        {
            couponModel.Id = Guid.NewGuid().ToString();
            var couponDTO = CouponHelper.ToDTO(couponModel) ?? throw new Exception("Error mapper from model to DTO");
            var query = new CreateCouponCommand(couponDTO);
            var coupon = await _mediator.Send(query);
            if (coupon == null)
            {
                throw new Exception("response is null");
            }
            return CouponHelper.ToModel(coupon);
        }

        public override async Task<CouponModel> GetCoupon(GetCouponRequest request, ServerCallContext context)
        {
            var query = new GetCouponByNameQuery(request.CouponName);
            var coupon = await _mediator.Send(query);
            var response = CouponHelper.ToModel(coupon);
            if (response == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Coupon with Name = {request.CouponName} is not found"));
            }
            return response;
        }
        public override async Task<UpdateCouponResponse> UpdateCoupon(CouponModel request, ServerCallContext context)
        {
            var couponDTO = CouponHelper.ToDTO(request) ?? throw new Exception("Mapper from Model to DTO");
            var query = new UpdateCouponCommand(couponDTO);
            var result = await _mediator.Send(query);
            return new UpdateCouponResponse
            {
                Success = result
            };
        }
        public override async Task<EmptyResponse> DeleteCoupon(DeleteCouponRequest request, ServerCallContext context)
        {
            var query = new DeleteCouponCommand(request.CouponName);
            await _mediator.Send(query);
            return new EmptyResponse();
        }

        public override async Task<ApplyCouponVoucherResponse> ApplyCouponVoucher(ApplyCouponVoucherRequest request, ServerCallContext context)
        {
            var querry = new GetTotalValueForOrderQuerry(request.CouponCode, (decimal)request.Amount);
            var result = await _mediator.Send(querry);

            if (result == null)
            {
                return new ApplyCouponVoucherResponse
                {
                    Id = string.Empty,
                    ValueDiscount = 0,
                    IsSuccess = false,
                    Message = "Coupon is not valid"
                };
            }
            return new ApplyCouponVoucherResponse
            {
                Id = result.Id,
                ValueDiscount = Convert.ToDouble(result),
                IsSuccess = true,
                Message = "Coupon is valid"
            };
        }

        public override async Task<ReturnCouponResponse> ReturnCoupon(ReturnCouponRequest request, ServerCallContext context)
        {
            var command = new ReturnCouponCommand
            {
                Id = request.Id
            };
            var result = await _mediator.Send(command);
            if (!result)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Coupon with Id = {request.Id} is not found"));
            }
            return new ReturnCouponResponse
            {
                Success = result
            };
        }

        public override async Task<GetCouponResponse> GetCoupons(EmptyRequest request, ServerCallContext context)
        {
            var query = new GetAllCouponQuery();
            var result = await _mediator.Send(query);
            var response = new GetCouponResponse();
            foreach (var item in result)
            {
                response.Coupons.Add(CouponHelper.ToModel(item));
            }
            return response;
        }
    }
}

