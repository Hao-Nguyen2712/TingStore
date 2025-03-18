using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Core.Entities
{
   public class PaymentTransaction
    {
        [Key]
        public Guid Id { get; set; } =  Guid.NewGuid();
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; }
        [StringLength(100)]
        public string? TransactionId { get; set; } // Mã giao dịch từ VNPay (nếu có)
        [Required] 
        [StringLength(20)]
        public string Status { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }  
        public DateTime? UpdatedAt { get; set; }  
    }
}
