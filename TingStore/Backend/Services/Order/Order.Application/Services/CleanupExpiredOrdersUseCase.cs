using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Queries;

namespace Order.Application.Services
{
    public class CleanupExpiredOrdersUseCase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CleanupExpiredOrdersUseCase> _logger;
        private readonly IDiscountClientService _discountClientService;

        public CleanupExpiredOrdersUseCase(IMediator mediator, ILogger<CleanupExpiredOrdersUseCase> logger, IDiscountClientService discountClientService)
        {
            _mediator = mediator;
            _logger = logger;
            _discountClientService = discountClientService;
        }


        public async Task ExecuteAsync()
        {
            var query = new GetCanceledOrdersQuery();
            var result = await _mediator.Send(query);
            if (result == null)
            {
                _logger.LogInformation("No orders to cleanup");
                return;
            }
            foreach (var order in result)
            {
                if (order.DiscountId == null)
                {
                    continue;
                }
                await _discountClientService.ReturnCoupon(order.DiscountId);
            }           
        }
    }
}
