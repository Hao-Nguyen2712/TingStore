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
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid? Id { get; set; }
        public int CustomerId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime OrderDate { get; set; } 
        public DateTime? UpdateAt { get; set; }
      
        public decimal TotalAmount { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? DiscountAmount { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal FinalAmount { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Status { get; set; } = OrderStatusDTO.Waiting.ToString();
        public List<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();
    }
}
