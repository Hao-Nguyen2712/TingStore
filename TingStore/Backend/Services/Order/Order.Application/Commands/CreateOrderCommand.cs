using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Order.Application.DTOs;

namespace Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<OrderDTO>
    {
        public string? Code { get; set; }
        public OrderDTO OrderDTO { get; set; }
        public CreateOrderCommand(OrderDTO orderDTO, string? code)
        {
            OrderDTO = orderDTO;
            this.Code = code;
        }
    }
}
