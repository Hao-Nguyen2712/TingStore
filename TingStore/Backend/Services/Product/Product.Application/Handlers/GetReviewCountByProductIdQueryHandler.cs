using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Queries;
using Product.Core.Repositories;

namespace Product.Application.Handlers
{
    public class GetReviewCountByProductIdQueryHandler : IRequestHandler<GetReviewCountByProductIdQuery, int>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetReviewCountByProductIdQueryHandler(IReviewRepository reviewRepository) => _reviewRepository = reviewRepository;

        public async Task<int> Handle(GetReviewCountByProductIdQuery request, CancellationToken cancellationToken)
        {
            return await _reviewRepository.GetReviewCountByProductId(request.ProductId);
        }
    }
}
