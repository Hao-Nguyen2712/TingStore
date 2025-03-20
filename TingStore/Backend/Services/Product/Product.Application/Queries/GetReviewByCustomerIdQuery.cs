using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Responses;

namespace Product.Application.Queries
{
    public class GetReviewByCustomerIdQuery : IRequest<IEnumerable<ReviewResponse>>
    {
        public int CustomerId { get; set; }

        public GetReviewByCustomerIdQuery(int customerId) => CustomerId = customerId;
    }
}
