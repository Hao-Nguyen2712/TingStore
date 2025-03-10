using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Commands;
using Product.Application.Queries;
using Product.Application.Responses;
using Product.Core.Models;

namespace Product.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ReviewController> _logger;

        public ReviewController(IMediator mediator, ILogger<ReviewController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: api/review/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReviewResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetReviewById(string id)
        {
            var query = new GetReviewByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result == null)
            {
                _logger.LogWarning("Review with ID {ReviewId} not found", id);
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/review/product/{productId}
        [HttpGet("product/{productId}")]
        [ProducesResponseType(typeof(IEnumerable<ReviewResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReviewByProductId(string productId)
        {
            var query = new GetReviewsByProductIdQuery(productId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET: api/review/customer/{customerId}
        [HttpGet("customer/{customerId}")]
        [ProducesResponseType(typeof(IEnumerable<ReviewResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReviewByCustomerId(string customerId)
        {
            var query = new GetReviewByCustomerIdQuery(customerId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // POST: api/review/product/{productId}
        [HttpPost("addReivew/{productId}")]
        [ProducesResponseType(typeof(ReviewResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddReivew(string productId, [FromBody] AddReviewCommand command)
        {
            command.ProductId = productId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // PUT: api/review/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateReview(string id, [FromBody] UpdateReviewCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            if (!result)
            {
                _logger.LogWarning("Failed to update Review with ID {ReviewId}", id);
                return NotFound();
            }
            return Ok(result);
        }


        // DELETE: api/review/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteReview(string id)
        {
            var query = new DeleteReviewCommand(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("product/{productId}/average-rating")]
        [ProducesResponseType(typeof(double), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAverageRatingByProductId(string productId)
        {
            var query = new GetAverageRatingByProductIdQuery(productId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("product/{productId}/review-count")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReviewCountByProductId(string productId)
        {
            var query = new GetReviewCountByProductIdQuery(productId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
