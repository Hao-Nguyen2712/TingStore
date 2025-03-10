using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Responses;

namespace Product.Application.Commands
{
    public class AddReviewCommand : IRequest<ReviewResponse>
    {
        public string ProductId { get; set; }
        public string CustomerId { get; set; }
        public int Rating { get; set; }
        public string? Comment  { get; set; }
    }
}
