using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        [Column(TypeName = "decimal(18,2)")]
        public decimal Value { get; set; } // giá trị giảm giá

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [NotMapped]
        public bool IsActive => EndDate > DateTime.Now && UsedCount < Quantity; // coupon có hoạt động không
        public bool IsReversed { get; set; } = false; // coupon được đặt chỗ chưa
        [Column(TypeName = "datetime2")]
        public DateTime? ReversedTime { get; set; } // thời gian đặt chỗ
                                                    
        public string? Description { get; set; } // mô tả coupon

        [Required]  
        public int Quantity { get; set; } // số lượng coupon
        public int UsedCount { get; set; } = 0; // số lượng coupon đã sử dụng
        [Column(TypeName = "decimal(18,2)")]
        public decimal? MinimumAmount { get; set; } // sử dụng đơn tối thiểu bao nhiêu ?
    }

}
