// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using Product.Core.Specs;
using TingStore.Client.Areas.User.Models.Products;

namespace TingStore.Client.Areas.User.Services.Products
{

    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiGateway");
        }

        public async Task<Pagination<ProductResponse>> GetAllProducts(ProductSpecParams productSpecParams)
        {
            var queryString = BuildQueryString(productSpecParams);
            var response = await _httpClient.GetAsync($"apigateway/product/GetAllProducts{queryString}");
            if(!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Unable to fetch products. Status: {response.StatusCode}, Error: {error}");
            }
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var pagination = JsonSerializer.Deserialize<Pagination<Product.Core.Models.Product>>(content, options)
                ?? throw new HttpRequestException("Unable to deserialize products.");

            // Ánh xạ từ Pagination<Product> sang Pagination<ProductResponse>
            var productResponses = pagination.Data.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                ProductImage = p.ProductImage,
                Description = p.Description,
                CategoryId = p.CategoryId,
                IsActive = p.IsActive,
                CreateAt = p.CreateAt,
                UpdateAt = p.UpdateAt
            }).ToList();

            return new Pagination<ProductResponse>(
                pageIndex: pagination.PageIndex,
                pageSize: pagination.PageSize,
                count: (int)pagination.Count,
                data: productResponses
            );
        }
        public async Task<ProductResponse> GetProductById(string id)
        {
            var response = await _httpClient.GetAsync($"apigateway/product/GetProductById/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Unable to fetch product. Status: {response.StatusCode}, Error: {errorContent}");
            }
            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var product = JsonSerializer.Deserialize<Product.Core.Models.Product>(content, options)
                ?? throw new HttpRequestException("Unable to deserialize product");

            // Ánh xạ từ Product sang ProductResponse
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                ProductImage = product.ProductImage,
                Description = product.Description,
                CategoryId = product.CategoryId,
                IsActive = product.IsActive,
                CreateAt = product.CreateAt,
                UpdateAt = product.UpdateAt,
                Images = product.Images?.Select(coreImage => new ProductImage
                {
                    Url = coreImage.ImageUrl
                }).ToList()
            };
        }

        private string BuildQueryString(ProductSpecParams productSpecParams)
        {
            var queryParams = new List<string>();
            if (productSpecParams.PageIndex > 0) queryParams.Add($"pageIndex={productSpecParams.PageIndex}");
            if (productSpecParams.PageSize > 0) queryParams.Add($"pageSize={productSpecParams.PageSize}");
            if (!string.IsNullOrEmpty(productSpecParams.BrandId)) queryParams.Add($"brandId={productSpecParams.BrandId}");
            if (!string.IsNullOrEmpty(productSpecParams.Sort)) queryParams.Add($"sort={productSpecParams.Sort}");
            if (!string.IsNullOrEmpty(productSpecParams.Search)) queryParams.Add($"search={productSpecParams.Search}");
            return queryParams.Any() ? $"?{string.Join("&", queryParams)}" : "";
        }
    }
}
