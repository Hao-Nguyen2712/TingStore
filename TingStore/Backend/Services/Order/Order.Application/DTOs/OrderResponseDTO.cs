using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.DTOs
{
    public class OrderResponseDTO
    {
        public string? Status { get; set; } = "Server Failed";
        public string? Message { get; set; } = "Order Failed";
        public Guid? OrderId { get; set; }
        public decimal? TotalAmount { get; set; }

        public OrderResponseDTO(string status , string message, Guid id , decimal total)
        {
            Status = status;
            Message = message;
            OrderId = id;
            TotalAmount = total;
        }

    }
}
