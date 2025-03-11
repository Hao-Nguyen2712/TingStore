using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Queries;
using Product.Core.Repositories;

namespace Product.Application.Handlers
{
    public class GetAverageRatingByProductIdQueryHandler : IRequestHandler<GetAverageRatingByProductIdQuery, double>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetAverageRatingByProductIdQueryHandler(IReviewRepository reviewRepository) => _reviewRepository = reviewRepository;

        public async Task<double> Handle(GetAverageRatingByProductIdQuery request, CancellationToken cancellationToken)
        {
            return await _reviewRepository.GetAverageRatingByProductId(request.ProductId);
        }
    }
}
