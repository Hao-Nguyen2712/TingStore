// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Product.Core.Models;

namespace TingStore.Client.Areas.User.Models.Products
{
    public class ProductResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ProductImage { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public List<ProductImage> Images { get; set; }
    }
    public class ProductImage
    {
        public string Url { get; set; } 
    }
}
