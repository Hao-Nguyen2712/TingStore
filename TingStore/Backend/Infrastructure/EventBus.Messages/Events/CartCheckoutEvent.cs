using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class CartCheckoutEvent 
    {
        public string? Code { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount => Items.Sum(item => item.Price * item.Quantity);

        public List<Item> Items { get; set; } = new List<Item>();

        public CartCheckoutEvent(string? code, int customerId, List<Item> items)
        {
            Code = code;
            CustomerId = customerId;
            Items = items;
        }

        public CartCheckoutEvent()
        {
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
