// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.Threading.Tasks;
using Product.Core.Specs;
using TingStore.Client.Areas.User.Models.Products;

namespace TingStore.Client.Areas.User.Services.Products
{
    public interface IProductService
    {
        Task<Pagination<ProductResponse>> GetAllProducts(ProductSpecParams productSpecParams);
        Task<ProductResponse> GetProductById(string id);
    }
}
