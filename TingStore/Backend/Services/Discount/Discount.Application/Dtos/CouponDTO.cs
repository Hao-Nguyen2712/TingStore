using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Dtos
{
    public class CouponDTO
    {
        public Guid Id { get; set; }
        public string? CouponName { get; set; }
        public string Code { get; set; }
        public decimal Value { get; set; } // giá trị giảm giá
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Description { get; set; } // mô tả coupon
        public int Quantity { get; set; } // số lượng coupon
        public int UsedCount { get; set; } = 0; // số lượng coupon đã sử dụng
        public decimal? MinimumAmount { get; set; }
    }
}
