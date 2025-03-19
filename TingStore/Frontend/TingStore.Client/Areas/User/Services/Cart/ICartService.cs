// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MongoDB.Driver;
using TingStore.Client.Areas.User.Models.Cart;

namespace TingStore.Client.Areas.User.Services.Cart
{
    public interface ICartService
    {
        public Task<bool> AddToCart(CartRequest cartRequest);
        public Task RemoveFromCart(string id);        
    }
}
