using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Responses;

namespace Product.Application.Commands
{
    public class DeleteReviewCommand : IRequest<bool>
    {
        public string Id { get; set; }

        public DeleteReviewCommand(string id) => Id = id;
    }
}
