using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Cart.Core.Entities;
using Cart.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Cart.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDistributedCache _distributedCache;
        private const string key = "user-";

        public CartRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task DeleteCart(string id)
        {
            await _distributedCache.RemoveAsync(key + id);
        }
        public async Task<CartShopping> GetCart(string id)
        {
            var cart = await _distributedCache.GetStringAsync(id.ToString());
            if (!string.IsNullOrEmpty(cart))
            {
                var response = JsonConvert.DeserializeObject<CartShopping>(cart) ?? throw new Exception("ErrorConver");
                return response;
            }
            throw new Exception("Cart not found");

        }
        public async Task<CartShopping> UpdateCart(CartShopping cart)
        {
            await _distributedCache.SetStringAsync(key + cart.Id, JsonConvert.SerializeObject(cart));
            return cart;
        }
    }
}

