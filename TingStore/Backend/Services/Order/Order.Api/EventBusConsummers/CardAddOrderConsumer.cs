// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Commands;
using Order.Application.DTOs;
using Order.Application.Mappers;

namespace Order.Api.EventBusConsummers
{
    public class CardAddOrderConsumer : IConsumer<CartCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<CardAddOrderConsumer> _logger;

        public CardAddOrderConsumer(IMediator mediator, IMapper mapper, ILogger<CardAddOrderConsumer> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<CartCheckoutEvent> context)
            {
            _logger.LogInformation("Create Order Event Consumed");
            try
            {
                var consume = context.Message;
                var command =_mapper.Map<CreateOrderCommand>(consume);
                var result = await _mediator.Send(command);

                _logger.LogInformation("Order created successfully. Order Id : {OrderId}", result.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
