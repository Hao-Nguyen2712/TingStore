using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using TingStore.Client.Areas.User.Models.UserProfile;
using TingStore.Client.Areas.User.Services.UserProfile;
using TingStore.Client.Areas.Admin.Models.Users;

namespace TingStore.Client.Areas.User.Services.UserProfile
{
    public class UserProfileService : IUserProfileService
    {
        private readonly HttpClient _httpClient;

        public UserProfileService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiGateway");
        }

        public async Task<UserProfileResponse> GetUserById(int id)
        {
            var response = await _httpClient.GetAsync($"apigateway/users/GetUserById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<UserProfileResponse>(content, options) ?? throw new HttpRequestException("User not found.");
            }
            throw new HttpRequestException("Unable to fetch user.");
        }

        public async Task<bool> UpdateUserProfile(UpdateUserProfileRequest user)
        {
            string data = JsonSerializer.Serialize(user);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync("apigateway/users/UpdateUser", content);
            return response.IsSuccessStatusCode;
        }
    }

}
