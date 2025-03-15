using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Order.Application.DTOs;

namespace Order.Application.Queries
{
    public class GetOrdersQuerry : IRequest<List<OrderDTO>>
    {
    }

}
