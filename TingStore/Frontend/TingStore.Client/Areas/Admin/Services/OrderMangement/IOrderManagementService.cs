using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TingStore.Client.Areas.Admin.Models.Order;

namespace TingStore.Client.Areas.Admin.Services.OrderMangement
{
    public interface IOrderManagementService
    {
        Task<IEnumerable<OrderResponse>> GetAllOrder();
        Task<OrderResponse> GetOrderByID(string id);

    }
}
