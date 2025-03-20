using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Product.Core.Models;
using Product.Core.Repositories;
using Product.Infrastructure.DbContext;

namespace Product.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IProductContext _context;

        public ReviewRepository(IProductContext context)
        {
            _context = context;
            // Tạo index cho ProductId và CustomerId
            var indexKeys = Builders<Review>.IndexKeys
                .Ascending(r => r.ProductId)
                .Ascending(r => r.CustomerId);
            var indexModel = new CreateIndexModel<Review>(indexKeys);
            _context.Reviews.Indexes.CreateOne(indexModel);
        }

        public async Task<Review> GetReviewById(string id)
        {
            return await _context.Reviews
            .Find(r => r.Id == id)
            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewByProductId(string productId)
        {
            return await _context.Reviews.
            Find(r => r.ProductId == productId).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewByCustomerId(int customerId)
        {
            return await _context.Reviews
            .Find(c => c.CustomerId == customerId).ToListAsync();
        }

        public async Task<Review> AddReview(Review review)
        {
            await _context.Reviews.InsertOneAsync(review);
            return review;
        }

        public async Task<bool> UpdateReview(string id, Review review)
        {
            if (review == null)
            {
                throw new ArgumentNullException(nameof(review), "Review can't be null");
            }

            var updateResult = await _context.Reviews.ReplaceOneAsync(r => r.Id == id, review);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteReview(string reviewId)
        {
            FilterDefinition<Review> filter = Builders<Review>.Filter.Eq(r => r.Id, reviewId);
            DeleteResult deleteResult = await _context.Reviews
            .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<double> GetAverageRatingByProductId(string productId)
        {
            if (string.IsNullOrEmpty(productId))
                throw new ArgumentNullException(nameof(productId), "Product ID cannot be null or empty");

            var avgRating = await _context.Reviews
            .Find(r => r.ProductId == productId)
            .Project(r => (double?)r.Rating).ToListAsync();

            return avgRating.Any() ? avgRating.Average() ?? 0.0 : 0.0;
        }


        public async Task<int> GetReviewCountByProductId(string productId)
        {
            var filter = Builders<Review>.Filter.Eq(r => r.ProductId, productId);
            return (int)await _context.Reviews.CountDocumentsAsync(filter);
        }
    }
}
