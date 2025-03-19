// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Product.Core.Models;
using TingStore.Client.Areas.User.Models.Reviews;

namespace TingStore.Client.Areas.User.Services.Reviews
{
    public interface IReviewProductService
    {
        Task<IEnumerable<ReviewResponse>> GetReviewsByProductId(string productId);
        Task<ReviewResponse> AddReview(ReviewRequest reviewRequest);
        Task<double> GetAverageRatingByProductId(string productId);
        Task<int> GetReviewCountByProductId(string productId);
        Task<bool> UpdateReview(string id, ReviewRequest ReviewRequest);
        Task<bool> DeleteReview(string reviewId);   
    }
}
