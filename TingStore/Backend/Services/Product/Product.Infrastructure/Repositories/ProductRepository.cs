// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Product.Core.Models;
using Product.Core.Repositories;
using Product.Core.Specs;
using Product.Infrastructure.DbContext;

namespace Product.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IProductContext _context;

        public ProductRepository(IProductContext productContext)
        {
            this._context = productContext;  
        }

        public async Task<bool> AddImageToProduct(ImageItem imageItem)
        {
            var filter = Builders<Core.Models.Product>.Filter.Eq(p => p.Id, imageItem.id);

            // Kiểm tra product tồn tại
            var product = await _context.Products.Find(filter).FirstOrDefaultAsync();
            if (product == null) return false;

            // Nếu Images == null, thì Set nó thành một mảng rỗng
            if (product.Images == null)
            {
                var setImagesUpdate = Builders<Core.Models.Product>.Update.Set(p => p.Images, new List<Core.Models.ProductImage>());
                await _context.Products.UpdateOneAsync(filter, setImagesUpdate);
            }

            // Sau đó, Push ảnh vào mảng Images
            var pushImageUpdate = Builders<Core.Models.Product>.Update.Push(p => p.Images, new Core.Models.ProductImage
            {
                ImageUrl = imageItem.imageUrl,
                IsPrimary = imageItem.isPrimary,
                CreateAt = DateTime.UtcNow
            });

            var updateResult = await _context.Products.UpdateOneAsync(filter, pushImageUpdate);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<Core.Models.Product> CreateProduct(Core.Models.Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return product;
        }
        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Core.Models.Product> filter = Builders<Core.Models.Product>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _context
                .Products
                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Core.Models.Product>> GetAllProduct()
        {
            return await _context.Products.Find(_ => true).ToListAsync();
        }

        public async Task<Core.Models.Product> GetProduct(string id)
        {
            return await _context
            .Products
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Core.Models.Product>> GetProductByBrand(string name)
        {
            FilterDefinition<Core.Models.Product> filter = Builders<Core.Models.Product>.Filter.Eq(p => p.CategoryId, name);
            return await _context
                .Products
                .Find(filter)
                .ToListAsync();
        }
        public async Task<IEnumerable<Core.Models.Product>> GetProductByName(string name)
        {
            //FilterDefinition<Core.Models.Product> filter = Builders<Core.Models.Product>.Filter.Eq(p => p.Name, name);
            var filter = Builders<Core.Models.Product>.Filter.Regex(p => p.Name, new BsonRegularExpression(name, "i"));
            return await _context
                .Products
                .Find(filter)
                .ToListAsync();
        }
        public async Task<Pagination<Core.Models.Product>> GetProducts(ProductSpecParams productSpecParams)
        {
            var builder = Builders<Product.Core.Models.Product>.Filter;
            var filter = builder.Eq(x => x.IsActive, true);

            if (!string.IsNullOrEmpty(productSpecParams.Search))
            {
                var searchFilter = builder.Regex(x => x.Name, new BsonRegularExpression(productSpecParams.Search, "i"));
                filter &= searchFilter;
            }
            if (!string.IsNullOrEmpty(productSpecParams.BrandId))
            {
                var brandFilter = builder.Eq(x => x.CategoryId, productSpecParams.BrandId);
                filter &= brandFilter;
            }

            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                return new Pagination<Product.Core.Models.Product>
                {
                    PageSize = productSpecParams.PageSize,
                    PageIndex = productSpecParams.PageIndex,
                    Data = await DataFilter(productSpecParams, filter),
                    Count = await _context.Products.CountDocumentsAsync(p =>
                        true) //TODO: Need to check while applying with UI
                };
            }

            return new Pagination<Product.Core.Models.Product>
            {
                PageSize = productSpecParams.PageSize,
                PageIndex = productSpecParams.PageIndex,
                Data = await _context
                    .Products
                    .Find(filter)
                    .Sort(Builders<Product.Core.Models.Product>.Sort.Ascending("Name"))
                    .Skip(productSpecParams.PageSize * (productSpecParams.PageIndex - 1))
                    .Limit(productSpecParams.PageSize)
                    .ToListAsync(),
                Count = await _context.Products.CountDocumentsAsync(p => true)
            };
        }
        public async Task<bool> UpdateProduct(Core.Models.Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product can't be null");
            }

            var filter = Builders<Core.Models.Product>.Filter.Eq(p => p.Id, product.Id);
            var update = Builders<Core.Models.Product>.Update
                .Set(p => p.Name, product.Name)
                .Set(p => p.Description, product.Description)
                .Set(p => p.Price, product.Price)
                .Set(p => p.Stock, product.Stock)
                .Set(p => p.UpdateAt, product.UpdateAt)
                .Set(p => p.CategoryId, product.CategoryId)
                .Set(p => p.IsActive, product.IsActive);

            var updateResult = await _context.Products.UpdateOneAsync(filter, update);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }


        private async Task<IReadOnlyList<Product.Core.Models.Product>> DataFilter(ProductSpecParams catalogSpecParams, FilterDefinition<Product.Core.Models.Product> filter)
        {
            switch (catalogSpecParams.Sort)
            {
                case "priceAsc":
                    return await _context
                        .Products
                        .Find(filter)
                        .Sort(Builders<Product.Core.Models.Product>.Sort.Ascending("Price"))
                        .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                        .Limit(catalogSpecParams.PageSize)
                        .ToListAsync();
                case "priceDesc":
                    return await _context
                        .Products
                        .Find(filter)
                        .Sort(Builders<Product.Core.Models.Product>.Sort.Descending("Price"))
                        .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                        .Limit(catalogSpecParams.PageSize)
                        .ToListAsync();
                default:
                    return await _context
                        .Products
                        .Find(filter)
                        .Sort(Builders<Product.Core.Models.Product>.Sort.Ascending("Name"))
                        .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                        .Limit(catalogSpecParams.PageSize)
                        .ToListAsync();
            }
        }
    }
}
