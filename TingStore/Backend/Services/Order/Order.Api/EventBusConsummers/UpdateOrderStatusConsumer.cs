// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Order.Application.Commands;
using Order.Application.DTOs;

namespace Order.Api.EventBusConsummers
{
    public class UpdateOrderStatusConsumer : IConsumer<UpdateOrderStatusEvent>
    {
        private readonly IMediator _mediator;
        private ILogger<UpdateOrderStatusConsumer> _logger;
        public UpdateOrderStatusConsumer(IMediator mediator, ILogger<UpdateOrderStatusConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<UpdateOrderStatusEvent> context)
        {
            _logger.LogInformation("UpdateOrderStatusConsumer Called");
            var id = context.Message.OrderId;

            if (context.Message.Status == "Success")
            {
                var command = new UpdateStatusOrderCommand(Guid.Parse(id), OrderStatusDTO.Successful.ToString());
                var result = await _mediator.Send(command);
                _logger.LogInformation("UpdateOrderStatusConsumer Completed");
            }
            else
            {
                var command = new UpdateStatusOrderCommand(Guid.Parse(id), OrderStatusDTO.Canceled.ToString());
                var result = await _mediator.Send(command);
                _logger.LogError("UpdateOrderStatusConsumer Error");
            }
            _logger.LogInformation("UpdateOrderStatusConsumer Finish");
        }
    }
}
