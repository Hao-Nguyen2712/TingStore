// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using System.Xml.Linq;
using TingStore.Client.Areas.Admin.Models.Categories;

namespace TingStore.Client.Areas.Admin.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiGateway");
        }
        public async Task<IEnumerable<CategoryResponse>> GetAllCategories()
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
            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<CategoryResponse>(content, options) ?? throw new HttpRequestException("Category not found.");
            }
            throw new HttpRequestException("Unable to fetch category.");
        }
        public async Task<IEnumerable<CategoryResponse>> GetCategoryByName(string name)
        {
            var response = await _httpClient.GetAsync($"apigateway/category/GetCategoryByName/{name}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<CategoryResponse>>(content, options) ?? throw new HttpRequestException("Category not found.");
            }
            throw new HttpRequestException("Unable to fetch category.");
        }
        public async Task<CategoryResponse> GetCategoryByNameInactive(string name)
        {
            var response = await _httpClient.GetAsync($"apigateway/category/GetCategoryByNameInactive/{name}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<CategoryResponse>(content, options) ?? throw new HttpRequestException("Category not found.");
            }
            throw new HttpRequestException("Unable to fetch category.");
        }
        public async Task<CategoryResponse> CreateCategory(CreateCategoryRequest category)
        {
            string data = JsonSerializer.Serialize(category);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("apigateway/category/CreateCategory", content);
            if (response.IsSuccessStatusCode)
            {
                var contentResult = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<CategoryResponse>(contentResult, options) ?? throw new HttpRequestException("Unable to create category.");
            }
            else
            {
                var errorResult = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Unable to create category. Status: {response.StatusCode}, Error: {errorResult}");
            }
        }
        public async Task<bool> UpdateCategory(UpdateCategoryRequest category)
        {
            string data = JsonSerializer.Serialize(category);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync("apigateway/category/UpdateCategory", content);
            if (response.IsSuccessStatusCode)
            {
                var contentResult = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<bool>(contentResult, options);
            }
            else
            {
                var errorResult = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Unable to delete category. Status: {response.StatusCode}, Error: {errorResult}");
            }
        }
        public async Task<bool> DeleteCategory(string id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"apigateway/category/DeleteCategory/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<bool>(content, options);
            }
            else
            {
                var errorResult = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Unable to delete category. Status: {response.StatusCode}, Error: {errorResult}");
            }
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllActiveCategories()
        {
            var response = await _httpClient.GetAsync($"apigateway/category/GetAllActiveCategories");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<CategoryResponse>>(content, options) ?? throw new HttpRequestException("Category not found.");
            }
            throw new HttpRequestException("Unable to fetch category.");
        }
        public async Task<IEnumerable<CategoryResponse>> GetAllInactiveCategories()
        {
            var response = await _httpClient.GetAsync($"apigateway/category/GetAllInactiveCategories");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<CategoryResponse>>(content, options) ?? throw new HttpRequestException("Category not found.");
            }
            throw new HttpRequestException("Unable to fetch category.");
        }
        public async Task<bool> RestoreCategory(string id)
        {
            var response = await _httpClient.PutAsync($"apigateway/category/RestoreCategory/{id}", null);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<int> GetCategoriesCount()
        {
            var categories = await GetAllCategories();
            return categories.Count();
        }
        public async Task<int> GetAllActiveCategoriesCount()
        {
            var activeCategory = await GetAllActiveCategories();
            return activeCategory.Count();
        }
        public async Task<int> GetAllInactiveCategoriesCount()
        {
            var inactiveCategory = await GetAllInactiveCategories();
            return inactiveCategory.Count();
        }
    }
}
