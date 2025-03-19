// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text;
using Newtonsoft.Json;
using TingStore.Client.Areas.User.Models.Cart;

namespace TingStore.Client.Areas.User.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string uri = "apigateway/cart";

        public CartService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> AddToCart(CartRequest cartRequest)
        {
            var client = _httpClientFactory.CreateClient("ApiGateway");
            client.Timeout = TimeSpan.FromMinutes(2);
           
            var request = new HttpRequestMessage(HttpMethod.Post, uri+"/CreateCart");
        
            request.Content = new StringContent(JsonConvert.SerializeObject(cartRequest), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {   
                return true;
            }
            return false;

        }
        public Task RemoveFromCart(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
