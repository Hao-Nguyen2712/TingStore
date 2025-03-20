// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using TingStore.Client.Areas.User.Models.Categories;

namespace TingStore.Client.Areas.User.Services.Categories
{
    public class CategoryUserService : ICategoryUserService
    {
        private readonly HttpClient _httpClient;

        public CategoryUserService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiGateway");
        }

        public async Task<IEnumerable<CategoryResponse>> GetCategories()
        {
            var response = await _httpClient.GetAsync("apigateway/category/GetAllCategories");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<CategoryResponse>>(content, options) ?? new List<CategoryResponse>();
            }
            throw new HttpRequestException("Unable to fetch inactive categories.");
        }
        public async Task<CategoryResponse> GetCategoryById(string id)
        {
            var response = await _httpClient.GetAsync($"apigateway/category/GetCategoryById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<CategoryResponse>(content, options) ?? throw new HttpRequestException("Category not found.");
            }
            throw new HttpRequestException("Unable to fetch category.");
        }
        public async Task<CategoryResponse> GetCategoryByName(string name)
        {
            var response = await _httpClient.GetAsync($"apigateway/category/GetCategoryByName/{name}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<CategoryResponse>(content, options) ?? throw new HttpRequestException("Category not found.");
            }
            throw new HttpRequestException("Unable to fetch category.");
        }
    }
}
