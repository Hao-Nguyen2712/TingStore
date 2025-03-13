using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Core.Entities
{
   public class CartShopping
    {
        public int Id { get; set; } //  UserId
        public List<CartShoppingItem> Items { get; set; } = new List<CartShoppingItem>();      
        public CartShopping(int id)
        {
            Id = id;
        }
    }
}
