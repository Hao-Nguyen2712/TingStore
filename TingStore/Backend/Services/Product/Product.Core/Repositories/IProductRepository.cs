// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product.Core.Models;
using Product.Core.Specs;

namespace Product.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Pagination<Product.Core.Models.Product>> GetProducts(ProductSpecParams productSpecParams);
        Task<Product.Core.Models.Product> GetProduct(string id);
        Task<IEnumerable<Product.Core.Models.Product>> GetProductByName(string name);
        Task<IEnumerable<Product.Core.Models.Product>> GetProductByBrand(string name);
        Task<Product.Core.Models.Product> CreateProduct(Product.Core.Models.Product product);
        Task<bool> UpdateProduct(Product.Core.Models.Product product);
        Task<bool> DeleteProduct(string id);
        Task<bool> AddImageToProduct(ImageItem imageItem);
    }
}
