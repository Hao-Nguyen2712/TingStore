using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Application.Responses
{
    // DTO trả về thông tin Review cho API
    public class ReviewResponse
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
