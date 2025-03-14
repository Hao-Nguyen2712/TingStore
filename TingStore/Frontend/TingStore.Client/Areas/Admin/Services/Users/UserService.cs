using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TingStore.Client.Areas.Admin.Models.Users;
using TingStore.Client.Areas.Admin.Services.Users;

namespace TingStore.Client.Areas.Admin.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiGateway");
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            var response = await _httpClient.GetAsync("apigateway/users/GetAllUsers");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<UserResponse>>(content, options) ?? new List<UserResponse>();
            }
            throw new HttpRequestException("Unable to fetch inactive users.");
        }

        public async Task<IEnumerable<UserResponse>> GetAllActiveUsers()
        {
            var response = await _httpClient.GetAsync("apigateway/users/GetAllActiveUsers");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<UserResponse>>(content, options) ?? new List<UserResponse>();
            }
            throw new HttpRequestException("Unable to fetch active users.");
        }

        public async Task<IEnumerable<UserResponse>> GetAllInactiveUsers()
        {
            var response = await _httpClient.GetAsync("apigateway/users/GetAllInactiveUsers");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<UserResponse>>(content, options) ?? new List<UserResponse>();
            }
            throw new HttpRequestException("Unable to fetch active users.");
        }

        public async Task<UserResponse> GetUserById(int id)
        {
            var response = await _httpClient.GetAsync($"apigateway/users/GetUserById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<UserResponse>(content, options) ?? throw new HttpRequestException("User not found.");
            }
            throw new HttpRequestException("Unable to fetch user.");
        }

        public async Task<UserResponse> GetUserByEmailAsync(string email)
        {
            var response = await _httpClient.GetAsync($"apigateway/users/GetUserByEmail/{email}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<UserResponse>(content, options) ?? throw new HttpRequestException("User not found.");
            }
            throw new HttpRequestException("Unable to fetch user.");
        }

        public async Task<UserResponse> CreateUser(CreateUserRequest user)
        {
            string data = JsonSerializer.Serialize(user);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("apigateway/users/CreateUser", content);
            if (response.IsSuccessStatusCode)
            {
                var contentResult = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<UserResponse>(contentResult, options) ?? throw new HttpRequestException("Unable to create user.");
            }
            throw new HttpRequestException("Unable to create user.");
        }

        public async Task<bool> UpdateUser(UpdateUserRequest user)
        {
            string data = JsonSerializer.Serialize(user);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync("apigateway/users/UpdateUser", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var response = await _httpClient.DeleteAsync($"apigateway/users/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RestoreUser(int id)
        {
            var response = await _httpClient.PutAsync($"api/users/RestoreUser/{id}", null);
            return response.IsSuccessStatusCode;
        }
    }
}
