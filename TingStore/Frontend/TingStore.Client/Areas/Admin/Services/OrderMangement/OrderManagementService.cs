using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TingStore.Client.Areas.Admin.Models.Order;

namespace TingStore.Client.Areas.Admin.Services.OrderMangement
{
    public class OrderManagementService : IOrderManagementService
    {
        private readonly HttpClient _httpClient;
        private string orderAPi;

        public OrderManagementService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiGateway");
            this.orderAPi = "/apigateway/order/";
        }

        public async Task<IEnumerable<OrderResponse>> GetAllOrder() {
            var response = await this._httpClient.GetAsync(this.orderAPi);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return  JsonSerializer.Deserialize<IEnumerable<OrderResponse>>(data, option) ?? new List<OrderResponse>();
            }
            throw new HttpRequestException("Unable to fetch All order.");
        }

        public async Task<int> GetAllOrderCount()
        {
            var orderCounts = await GetAllOrder();
            return orderCounts.Count();
        }

        public async Task<OrderResponse> GetOrderByID(string id) {
            var respone = await this._httpClient.GetAsync(this.orderAPi+"GetOrderByOrderId/"+id);
            if(respone.IsSuccessStatusCode) {
                var data = await respone.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};
                return JsonSerializer.Deserialize<OrderResponse>(data, option);
            }
            throw new HttpRequestException("Unable to fetch order detail");
        }
    }
}
