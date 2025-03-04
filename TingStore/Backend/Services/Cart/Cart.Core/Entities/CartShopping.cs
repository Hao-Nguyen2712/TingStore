using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Core.Entities
{
   public class CartShopping
    {
        public string Id { get; set; }
        public List<CartShoppingItem> Items { get; set; } = new List<CartShoppingItem>();
       
        public CartShopping(string id)
        {
            Id = id;
        }
    }
}
