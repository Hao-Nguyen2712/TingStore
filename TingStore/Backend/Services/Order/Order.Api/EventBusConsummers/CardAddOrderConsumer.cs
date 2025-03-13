// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Commands;

namespace Order.Api.EventBusConsummers
{
    public class CardAddOrderConsumer : IConsumer<CartCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<CardAddOrderConsumer> _logger;

        public CardAddOrderConsumer(IMediator mediator , IMapper mapper,ILogger<CardAddOrderConsumer> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<CartCheckoutEvent> context)
        {
            var command = _mapper.Map<CreateOrderCommand>(context.Message);
            var result = await _mediator.Send(command);
            if(result != null)
            {
                _logger.LogInformation("Order added Successfull");
            }
        }
    }
}
