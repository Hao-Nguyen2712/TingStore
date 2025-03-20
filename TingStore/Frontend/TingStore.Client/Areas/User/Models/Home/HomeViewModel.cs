// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TingStore.Client.Areas.User.Models.Categories;
using TingStore.Client.Areas.User.Models.Products;

namespace TingStore.Client.Areas.User.Models.Home
{
    public class HomeViewModel
    {
        public List<CategoryResponse> Categories { get; set; }
        public List<ProductResponse> Products { get; set; }
    }
}
