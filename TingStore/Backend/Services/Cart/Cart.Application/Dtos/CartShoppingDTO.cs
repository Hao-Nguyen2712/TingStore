using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Core.Entities;

namespace Cart.Application.Dtos
{
    public class CartShoppingDTO
    {
        public string Id { get; set; }
        public List<CartShoppingItemDTO> Items { get; set; } = new List<CartShoppingItemDTO>();
    }
}
