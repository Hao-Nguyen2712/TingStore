using System.Net.WebSockets;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payment.Application.Commands;
using Payment.Application.Dtos;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;
using VNPAY.NET.Utilities;

namespace Payment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly VNPAY.NET.IVnpay _vnpay;
        private readonly IConfiguration _configuration;
        private readonly IPublishEndpoint _publishEndpoint;
        public PaymentController(IMediator mediator, VNPAY.NET.IVnpay vnpay, IConfiguration configuration, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator;
            _vnpay = vnpay;
            _configuration = configuration;
            _publishEndpoint = publishEndpoint;
            _vnpay.Initialize(_configuration["Vnpay:TmnCode"], _configuration["Vnpay:HashSecret"], _configuration["Vnpay:Url"], _configuration["Vnpay:ReturnUrl"]);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTransactionPayment([FromBody] CreatePaymentRequest request)
        {
            var command = new AddTransactionCommand
            {
                OrderId = request.OrderID,
                Amount = request.Amount
            };
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return BadRequest();
            }
            var ip = NetworkHelper.GetIpAddress(HttpContext);
            var paymentRequest = new PaymentRequest
            {
                PaymentId = DateTime.Now.Ticks,
                Money = (double)result.Amount,
                Description = result.Id.ToString(),
                IpAddress = ip,
                BankCode = BankCode.ANY,
                Currency = Currency.VND,
                Language = DisplayLanguage.Vietnamese

            };
            var paymentUrl = _vnpay.GetPaymentUrl(paymentRequest);
            return Ok(paymentUrl);
        }


        [HttpGet]
        [Route("PaymentCallBack")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PaymentCallback()
        {
            if (Request.QueryString.HasValue)
            {
                var payment = _vnpay.GetPaymentResult(Request.Query);
                if (payment.IsSuccess)
                {
                    var id = payment.Description.ToString();
                    var command = new UpdateTransactionCommand(Guid.Parse(id), payment.PaymentId.ToString(), PaymentStatusDTO.Success.ToString());
                    var result = await _mediator.Send(command);
                    if (String.IsNullOrEmpty(result))
                    {
                        return BadRequest();
                    }
                    var message = new EventBus.Messages.Events.UpdateOrderStatusEvent
                    {
                        OrderId = result,
                        Status = PaymentStatusDTO.Success.ToString()
                    };
                    await _publishEndpoint.Publish(message);
                }
                else
                {
                    var id = payment.Description.ToString();
                    var command = new UpdateTransactionCommand(Guid.Parse(id), payment.PaymentId.ToString(), PaymentStatusDTO.Failed.ToString());
                    var result = await _mediator.Send(command);
                    if (!String.IsNullOrEmpty(result))
                    {
                        return BadRequest();
                    }
                    var message = new EventBus.Messages.Events.UpdateOrderStatusEvent
                    {
                        OrderId = result,
                        Status = PaymentStatusDTO.Failed.ToString()
                    };
                    await _publishEndpoint.Publish(message);
                }
            }
            return Accepted();

        }
    }
}

