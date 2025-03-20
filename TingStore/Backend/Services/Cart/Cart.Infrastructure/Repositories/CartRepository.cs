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
        public async Task DeleteCart(int id)
        {
            await _distributedCache.RemoveAsync(key + id);
        }
        public async Task<CartShopping> GetCart(int id)
        {
            var cart = await _distributedCache.GetStringAsync(key + id.ToString());
            if (!string.IsNullOrEmpty(cart))
            {
                var response = JsonConvert.DeserializeObject<List<CartShoppingItem>>(cart) ?? throw new Exception("ErrorConver");
                var result = new CartShopping(id);
                result.Items = response;
                return result;
            }
            throw new Exception("Cart not found");

        }
        public async Task<CartShopping> UpdateCart(CartShopping cart)
        {
            var checkCart = await _distributedCache.GetStringAsync(key + cart.Id);
            if (string.IsNullOrEmpty(checkCart))
            {
                await _distributedCache.SetStringAsync(key + cart.Id, JsonConvert.SerializeObject(cart.Items));
                return cart;
            }

            var existingItems = JsonConvert.DeserializeObject<List<CartShoppingItem>>(checkCart);
            foreach (var newItem in cart.Items)
            {
                var existingItem = existingItems.FirstOrDefault(i => i.ProductId == newItem.ProductId);
                if (existingItem == null)
                {
                    existingItems.Add(newItem);
                }
                else
                {
                    existingItem.Quantity = newItem.Quantity;
                }
            }
            await _distributedCache.SetStringAsync(key + cart.Id, JsonConvert.SerializeObject(existingItems));
            cart.Items = existingItems;
            return cart;
        }

        public async Task<CartShopping> AddCart(CartShopping cart)
        {
            var checkCart = await _distributedCache.GetStringAsync(key + cart.Id);
            if (string.IsNullOrEmpty(checkCart))
            {
                await _distributedCache.SetStringAsync(key + cart.Id, JsonConvert.SerializeObject(cart.Items));
                return cart;
            }

            var existingItems = JsonConvert.DeserializeObject<List<CartShoppingItem>>(checkCart);
            foreach (var newItem in cart.Items)
            {
                var existingItem = existingItems.FirstOrDefault(i => i.ProductId == newItem.ProductId);
                if (existingItem == null)
                {
                    existingItems.Add(newItem);
                }
                else
                {
                    existingItem.Quantity += newItem.Quantity;
                }
            }
            await _distributedCache.SetStringAsync(key + cart.Id, JsonConvert.SerializeObject(existingItems));
            cart.Items = existingItems;
            return cart;
        }

        public async Task<bool> RemoveProductFromCart(int cartId, List<string> productIdsToRemove)
        {
            string cacheKey = key + cartId;
            var checkCart = await _distributedCache.GetStringAsync(cacheKey);


            if (string.IsNullOrEmpty(checkCart))
            {
                return false;

            }
            var existingItems = JsonConvert.DeserializeObject<List<CartShoppingItem>>(checkCart);
            if (existingItems == null)
            {
                return false;
            }

            var updatedItems = existingItems.Where(item => !productIdsToRemove.Contains(item.ProductId)).ToList();
            for (int i = 0; i < updatedItems.Count; i++)
            {
                updatedItems.RemoveAt(i);
                break;
            }

            if (updatedItems.Count == 0)
            {
                await _distributedCache.RemoveAsync(cacheKey);
            }
            else
            {
                await _distributedCache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(updatedItems));
            }

            return true;
        }


        public async Task<CartShopping> CheckOut(int userId, List<String> proId)
        {
            string cacheKey = key + userId;
            var checkCart = await _distributedCache.GetStringAsync(cacheKey);
            if (string.IsNullOrEmpty(checkCart))
            {
                return null;
            }

            var existingItems = JsonConvert.DeserializeObject<List<CartShoppingItem>>(checkCart);
            if (existingItems == null)
            {
                return null;
            }

            var productCheckout = existingItems.Where(item => proId.Contains(item.ProductId)).ToList();

            var cart = new CartShopping(userId);
            cart.Items = productCheckout;
            return cart;
        }
    }
}

