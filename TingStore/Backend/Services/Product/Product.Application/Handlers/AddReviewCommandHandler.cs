using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Commands;
using Product.Application.Mappers;
using Product.Application.Responses;
using Product.Core.Models;
using Product.Core.Repositories;

namespace Product.Application.Handlers
{
    public class AddReviewCommandHandler : IRequestHandler<AddReviewCommand, ReviewResponse>
    {
        private readonly IReviewRepository _reviewRepository;

        public AddReviewCommandHandler(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<ReviewResponse> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var reviewEntity = ReviewMapper.Mapper.Map<Review>(request) ??
            throw new ApplicationException("Mapping failed while creating a new user.");
            reviewEntity.CreateAt = DateTime.UtcNow;

            await _reviewRepository.AddReview(reviewEntity);
            return ReviewMapper.Mapper.Map<ReviewResponse>(reviewEntity);
        }
    }
}
