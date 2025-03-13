using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class CartCheckoutEvent 
    {
        public string? Code { get; set; }
        public int CustomerId { get; set; }
        public int ProId { get; set; }
        public decimal TotalAmount { get; set; } = 0;
        public List<CartItemCheckoutEvent> Items { get; set; } = new List<CartItemCheckoutEvent>();



    }
}
