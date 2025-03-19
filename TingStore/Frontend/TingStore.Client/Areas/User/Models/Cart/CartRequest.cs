// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TingStore.Client.Areas.User.Models.Cart
{
    public class CartRequest
    {
        public int Id { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public CartRequest() { }
        public CartRequest(int id, List<CartItem> items)
        {
            Id = id;
            Items = items;
        }
    }

    public class CartItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductImage { get; set; }
        public string ProductName { get; set; }

    }
}
