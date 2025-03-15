using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Order.Application.DTOs;

namespace Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<OrderDTO>
    {
        public string? Code { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();


        public CreateOrderCommand()
        {
            
        }

        public CreateOrderCommand(string? Code, int CustomerId, decimal TotalAmout, List<Item> Item)
        {
            this.Code = Code;
            this.CustomerId = CustomerId;
            this.TotalAmount = TotalAmout;
            this.Items = Items;
        }
    }

    public class Item
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
