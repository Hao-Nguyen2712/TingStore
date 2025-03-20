using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Core.Entities;
using System.Text.Json.Serialization;

namespace Order.Application.DTOs
{
   public class OrderDTO
    { 
        public Guid? Id { get; set; }
        public int CustomerId { get; set; }
     
        public DateTime OrderDate { get; set; } 
        public DateTime? UpdateAt { get; set; }
      
        public decimal TotalAmount { get; set; }

        public string? DiscountId { get; set; }
        public decimal? DiscountAmount { get; set; }
   
        public decimal FinalAmount { get; set; }
     
        public string? Status { get; set; } = OrderStatusDTO.Waiting.ToString();
        public List<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();
    }
}
