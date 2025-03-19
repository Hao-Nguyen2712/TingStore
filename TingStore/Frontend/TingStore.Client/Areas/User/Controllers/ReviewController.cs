using Microsoft.AspNetCore.Mvc;
using TingStore.Client.Areas.User.Models.Reviews;
using TingStore.Client.Areas.User.Services.Reviews;

namespace TingStore.Client.Areas.User.Controllers
{
    [Area("User")]
    [Route("[area]/[controller]/[action]")] 
    public class ReviewController : Controller
    {
        private readonly IReviewProductService _reviewProductService;

        public ReviewController(IReviewProductService reviewProductService)
        {
            _reviewProductService = reviewProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReview(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return BadRequest("Product ID is required.");
            }
            try
            {
                var reviews = await _reviewProductService.GetReviewsByProductId(productId);
                return Json(reviews);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequest reviewRequest)
        {
            if (reviewRequest == null || string.IsNullOrEmpty(reviewRequest.ProductId) || reviewRequest.Rating <= 0 || reviewRequest.Rating > 5)
            {
                return BadRequest("Product ID and a valid rating (1-5) are required.");
            }

            try
            {
                var result = await _reviewProductService.AddReview(reviewRequest);
                return Json(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { error = $"Failed to add review: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(string id, [FromBody] ReviewRequest reviewRequest)
        {
            if (string.IsNullOrEmpty(id) || reviewRequest == null || reviewRequest.Rating <= 0 || reviewRequest.Rating > 5)
            {
                return BadRequest("Review ID and a valid rating (1-5) are required");
            }
            try
            {
                var result = await _reviewProductService.UpdateReview(id, reviewRequest);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Review ID is required.");
            }
            try
            {
                var result = await _reviewProductService.DeleteReview(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetAverageRating(string productId)
        {
            var result = await _reviewProductService.GetAverageRatingByProductId(productId);
            return View(result);
        }


        [HttpGet("{productId}")]
        public async Task<IActionResult> ReviewCount(string productId)
        {
            var result = await _reviewProductService.GetReviewCountByProductId(productId);
            return View(result);
        }
    }
}
