// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json;
using TingStore.Client.Areas.User.Models.Categories;

namespace TingStore.Client.Areas.User.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiGateway");
        }

        public async Task<IEnumerable<CategoryResponse>> GeAllActiveCategories() {
            var response = await this._httpClient.GetAsync($"apigateway/category/GetAllActiveCategories");
            if(response.IsSuccessStatusCode) {
                var data = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<IEnumerable<CategoryResponse>>(data, option) ?? new List<CategoryResponse>();
            }
            throw new HttpRequestException("Unable to fetch Product List.");
        }

        public async Task<List<string>> GeAllActiveCategoriesListString() {
            List<string> listCategory = new List<string>();
            var categorys = await GeAllActiveCategories();
            if(categorys.Count() != 0 || !categorys.Any()) {
                foreach(var item in categorys) {
                    listCategory.Add(item.Name);
                }
                return listCategory;
            }
            return new List<string>();
        }
        public Task<IEnumerable<CategoryResponse>> GetCategories() => throw new NotImplementedException();
        public Task<CategoryResponse> GetCategoryById(string id) => throw new NotImplementedException();
        public Task<CategoryResponse> GetCategoryByName(string name) => throw new NotImplementedException();
    }
}
