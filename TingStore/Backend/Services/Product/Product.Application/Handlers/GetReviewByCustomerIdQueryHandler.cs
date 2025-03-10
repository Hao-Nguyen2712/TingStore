using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Mappers;
using Product.Application.Queries;
using Product.Application.Responses;
using Product.Core.Repositories;

namespace Product.Application.Handlers
{
    public class GetReviewByCustomerIdQueryHandler : IRequestHandler<GetReviewByCustomerIdQuery, IEnumerable<ReviewResponse>>
    {
        private readonly IReviewRepository _reviewRepository;

        public GetReviewByCustomerIdQueryHandler(IReviewRepository reviewRepository) => _reviewRepository = reviewRepository;

        public async Task<IEnumerable<ReviewResponse>> Handle(GetReviewByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _reviewRepository.GetReviewByCustomerId(request.CustomerId);
            return ReviewMapper.Mapper.Map<IEnumerable<ReviewResponse>>(reviews);
        }
    }
}
