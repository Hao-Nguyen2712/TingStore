using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Core.Models;

namespace Product.Core.Repositories
{
    public interface IReviewRepository
    {
        Task<Review> GetReviewById(string id);
        Task<IEnumerable<Review>> GetReviewByProductId(string productId);
        Task<IEnumerable<Review>> GetReviewByCustomerId(string customerId);
        Task<Review> AddReview(Review review);
        Task<bool> UpdateReview(string id, Review review);
        Task<bool> DeleteReview(string id);
        Task<double> GetAverageRatingByProductId(string productId);
        Task<int> GetReviewCountByProductId(string productId);
    }
}
