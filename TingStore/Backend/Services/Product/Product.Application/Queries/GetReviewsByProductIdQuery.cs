using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Responses;

namespace Product.Application.Queries
{
    public class GetReviewsByProductIdQuery : IRequest<IEnumerable<ReviewResponse>>
    {
        public string ProductId { get; set; }

        public GetReviewsByProductIdQuery(string productId) => ProductId = productId;
    }
}
