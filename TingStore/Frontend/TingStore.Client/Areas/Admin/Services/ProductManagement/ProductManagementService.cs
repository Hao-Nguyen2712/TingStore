using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TingStore.Client.Areas.Admin.Models.Products;
using TingStore.Client.Areas.Admin.Models.Products.Specs;

namespace TingStore.Client.Areas.Admin.Services.ProductManagement
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly HttpClient _httpClient;
        private string productAPi;

        public ProductManagementService(IHttpClientFactory httpClientFactory)
        {
            this._httpClient = httpClientFactory.CreateClient("ApiGateway");
            this.productAPi = "/apigateway/product/";
        }

        public async Task<bool> AddProductImage(string id, List<IFormFile> files)
        {
            using var formData = new MultipartFormDataContent();

            foreach (var file in files)
            {
                var fileStream = file.OpenReadStream();
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                formData.Add(fileContent, "files", file.FileName);
            }

            HttpResponseMessage response = await _httpClient.PostAsync($"{this.productAPi}AddImageProduct?id={id}&isPrimary=true", formData);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                throw new HttpRequestException($"Unable to add product images. Status: {response.StatusCode}");
            }
        }

        public async Task<ProductResponse> CreateProduct(ProductResquest productResquest)
        {
            using var formData = new MultipartFormDataContent();

            formData.Add(new StringContent(productResquest.Name), "Name");
            formData.Add(new StringContent(productResquest.CategoryId.ToString()), "CategoryId");
            formData.Add(new StringContent(productResquest.Description), "Description");
            formData.Add(new StringContent(productResquest.Price.ToString()), "Price");
            formData.Add(new StringContent(productResquest.Stock.ToString()), "Stock");
            formData.Add(new StringContent(productResquest.IsActive.ToString()), "IsActive");

            if (productResquest.ProductFile != null)
            {
                var fileStream = productResquest.ProductFile.OpenReadStream();
                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(productResquest.ProductFile.ContentType);
                formData.Add(fileContent, "ProductFile", productResquest.ProductFile.FileName);
            }

            HttpResponseMessage response = await _httpClient.PostAsync(this.productAPi + "CreateProduct", formData);

            if (response.IsSuccessStatusCode)
            {
                var contentResult = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<ProductResponse>(contentResult, options) ?? throw new HttpRequestException("Unable to create product.");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error Response: " + errorContent);
                throw new HttpRequestException($"Unable to create product. Status: {response.StatusCode}, Error: {errorContent}");
            }
        }

        public async Task<bool> DeleteProduct(string id) {
            var response = await _httpClient.DeleteAsync(this.productAPi+"DeleteProduct/"+id);
            return response.IsSuccessStatusCode;
        }

        public async Task<Pagination<ProductResponse>> GetAllProducts(int indexPage, string sort)
        {
            var response = await this._httpClient.GetAsync(this.productAPi + "GetAllProducts?PageIndex=" + indexPage + "&Sort=" + sort);
            if (response.IsSuccessStatusCode)
            {
                var contentType = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<Pagination<ProductResponse>>(contentType, option) ?? new Pagination<ProductResponse>();
            }
            else
            {
                throw new HttpRequestException("Unable to fetch inactive users.");
            }
        }

        public async Task<ProductResponse> GetProductById(string id) {
            if(string.IsNullOrEmpty(id)) {
                throw new HttpRequestException("Product id is null");
            }
            var response = await this._httpClient.GetAsync(this.productAPi+"GetProductById/"+ id);
            if(response.IsSuccessStatusCode) {
                var data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<ProductResponse>(data, options) ?? new ProductResponse();
            }
            throw new HttpRequestException("Unable to fetch inactive product.");
        }

        public async Task<bool> UpdateProduct(UpdateProductResquest updateProduct) {
            string data = JsonSerializer.Serialize(updateProduct);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            var response = await this._httpClient.PutAsync(this.productAPi+"UpdateProduct", content);
            if(response.IsSuccessStatusCode) {
                return true;
            }
            return false;
        }
    }
}
