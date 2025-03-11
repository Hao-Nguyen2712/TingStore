using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Commands;
using Product.Core.Repositories;

namespace Product.Application.Handlers
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, bool>
    {
        private readonly IReviewRepository _reviewRepository;

        public UpdateReviewCommandHandler(IReviewRepository reviewRepository) => _reviewRepository = reviewRepository;

        public async Task<bool> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await _reviewRepository.GetReviewById(request.Id);
            if (review == null)
                throw new KeyNotFoundException($"Review with ID {request.Id} not found");

            review.Rating = request.Rating;
            review.Comment = request.Comment;
            review.UpdateAt = DateTime.UtcNow;

            return await _reviewRepository.UpdateReview(request.Id, review);
        }
    }
}
