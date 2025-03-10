using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Product.Application.Queries
{
    public class GetReviewCountByProductIdQuery : IRequest<int>
    {
        public string ProductId { get; set; }

        public GetReviewCountByProductIdQuery(string productId) => ProductId = productId;
    }
}
