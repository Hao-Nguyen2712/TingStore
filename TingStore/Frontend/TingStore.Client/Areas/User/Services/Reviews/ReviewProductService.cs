using System.Text;
using System.Text.Json;
using TingStore.Client.Areas.User.Models.Reviews;

namespace TingStore.Client.Areas.User.Services.Reviews
{
    public class ReviewProductService : IReviewProductService
    {
        private readonly HttpClient _httpClient;

        public ReviewProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiGateway");
        }

        public async Task<IEnumerable<ReviewResponse>> GetReviewsByProductId(string productId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"apigateway/review/product/{productId}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<IEnumerable<ReviewResponse>>(content, options) ?? Enumerable.Empty<ReviewResponse>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching reviews for productId {productId}: {ex.Message}");
                return Enumerable.Empty<ReviewResponse>();
            }
        }

        public async Task<ReviewResponse> AddReview(ReviewRequest reviewRequest)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(reviewRequest), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"apigateway/review/addReview/{reviewRequest.ProductId}", content);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<ReviewResponse>(data, options) ?? new ReviewResponse();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error adding review for productId {reviewRequest.ProductId}: {ex.Message}");
                return new ReviewResponse(); 
            }
        }

        public async Task<bool> UpdateReview(string id, ReviewRequest reviewRequest)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(reviewRequest), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"apigateway/review/{id}", content);
                response?.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error updating review {id}: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteReview(string reviewId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"apigateway/review/{reviewId}");
                response?.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error deleting review {reviewId}: {ex.Message}");
                return false;
            }
        }

        public async Task<double> GetAverageRatingByProductId(string productId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"apigateway/review/product/{productId}/average-rating");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<double>(content);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching average rating for productId {productId}: {ex.Message}");
                return 0.0; 
            }
        }

        public async Task<int> GetReviewCountByProductId(string productId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"apigateway/review/product/{productId}/review-count");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<int>(content);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching review count for productId {productId}: {ex.Message}");
                return 0; 
            }
        }
    }
}
