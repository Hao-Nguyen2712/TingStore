using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Core.Entities;

namespace Order.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<Order.Core.Entities.Order> AddOrder(Order.Core.Entities.Order order);
        Task<Order.Core.Entities.Order> GetOrderById(Guid orderId);
        Task<IEnumerable<Order.Core.Entities.Order>> GetOrdersByCustomerId(int customerid);
        Task<Order.Core.Entities.Order> UpdateOrder(Order.Core.Entities.Order order);
        Task<bool> DeleteOrder(Guid orderId);
        Task<IEnumerable<Core.Entities.Order>> GetOrders();
        Task<bool> UpdateOrderStatus(Guid orderId, string newStatus);
        Task<IEnumerable<Core.Entities.Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate);


    }
}
