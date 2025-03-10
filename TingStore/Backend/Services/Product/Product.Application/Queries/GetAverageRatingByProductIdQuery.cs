using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Product.Application.Queries
{
    public class GetAverageRatingByProductIdQuery : IRequest<double>
    {
        public string ProductId { get; set; }

        public GetAverageRatingByProductIdQuery(string productId) => ProductId = productId;
    }
}
