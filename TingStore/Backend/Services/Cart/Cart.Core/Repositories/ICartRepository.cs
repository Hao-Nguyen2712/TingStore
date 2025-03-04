using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cart.Core.Entities;

namespace Cart.Core.Repositories
{
    public interface ICartRepository
    {
        Task<CartShopping> GetCart(string id);
        Task<CartShopping> UpdateCart(CartShopping cart);
        Task DeleteCart(string id);
    }
}
