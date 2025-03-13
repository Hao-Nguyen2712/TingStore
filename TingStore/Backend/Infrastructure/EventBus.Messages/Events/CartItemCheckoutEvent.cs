using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
   public class CartItemCheckoutEvent
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        
        public CartItemCheckoutEvent(string proId ,  string proName , int proQuan , decimal proPrice)
        {
            ProductId = proId;
            ProductName = proName;
            Quantity = proQuan;
            Price = proPrice;
        }
    }
}
