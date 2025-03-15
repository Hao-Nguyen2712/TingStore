using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Order.Application.DTOs;

namespace Order.Application.Queries
{
    public class GetOrderByCustomerIdQuerry : IRequest<List<OrderDTO>>
    {
        public int CustomerId { get; set; }
        public GetOrderByCustomerIdQuerry(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
