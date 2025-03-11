using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Responses;

namespace Product.Application.Queries
{
    public class GetReviewByIdQuery : IRequest<ReviewResponse>
    {
        public string Id { get; set; }

        public GetReviewByIdQuery(string id) => Id = id;
    }
}
