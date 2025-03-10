using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Responses;

namespace Product.Application.Commands
{
    public class UpdateReviewCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }

        public UpdateReviewCommand(string id, int rating, string? comment = null)
        {
            Id = id;
            Rating = rating;
            Comment = comment;;
        }
    }
}
