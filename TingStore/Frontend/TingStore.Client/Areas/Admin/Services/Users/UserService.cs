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

        public async Task<UserResponse> GetUserByEmail(string email)
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
            Console.WriteLine("Sending data: " + data);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("apigateway/users/CreateUser", content);
            if (response.IsSuccessStatusCode)
            {
                var contentResult = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response: " + contentResult);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<UserResponse>(contentResult, options) ?? throw new HttpRequestException("Unable to create user.");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error Response: " + errorContent); 
                throw new HttpRequestException($"Unable to create user. Status: {response.StatusCode}, Error: {errorContent}");
            }
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
            var response = await _httpClient.PutAsync($"apigateway/users/RestoreUser/{id}", null);
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

        public async Task<int> GetAllUsersCount()
        {
            var users = await GetAllUsers();
            return users.Count();
        }

        public async Task<int> GetAllActiveUsersCount()
        {
            var activeUsers = await GetAllActiveUsers();
            return activeUsers.Count();
        }

        public async Task<int> GetAllInactiveUsersCount()
        {
            var inactiveUsers = await GetAllInactiveUsers();
            return inactiveUsers.Count();
        }
    }
}
