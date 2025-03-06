using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Core.Entities
{
    public class Coupon
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string? CouponName { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        public decimal Value { get; set; } // giá trị giảm giá

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
        public string? Description { get; set; } // mô tả coupon

        [Required]
        public int Quantity { get; set; } // số lượng coupon
        public int UsedCount { get; set; } = 0; // số lượng coupon đã sử dụng
        public decimal? MinimumAmount { get; set; } // sử dụng đơn tối thiểu bao nhiêu ?
    }
}
