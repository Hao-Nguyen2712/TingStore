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
        Task<CartShopping> GetCart(int id);
        Task<CartShopping> UpdateCart(CartShopping cart);
        Task DeleteCart(int id);
        Task<bool> RemoveProductFromCart(int cartId, List<string> productIdsToRemove);
        Task<CartShopping> CheckOut(int userId, List<String> proId);
    }
}
