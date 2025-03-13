using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Order.Core.Repositories;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context;
        }
        public async Task<Core.Entities.Order> AddOrder(Core.Entities.Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException();
            }
            order.FinalAmount = order.TotalAmount - (order.DiscountAmount ?? 0);

            if (order.OrderItems != null && order.OrderItems.Any())
            {
                foreach (var item in order.OrderItems)
                {
                    item.OrderId = order.Id;
                }
            }

            await _context.AddAsync(order);
            await _context.SaveChangesAsync();
            return await GetOrderById(order.Id);
        }
        public Task DeleteOrder(Guid orderId) => throw new NotImplementedException();
        public async Task<Core.Entities.Order> GetOrderById(Guid orderId)
        {
            return await _context.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.Id == orderId);
        }

        public async Task<IEnumerable<Core.Entities.Order>> GetOrders() => await _context.Orders.ToListAsync();

        public async Task<IEnumerable<Core.Entities.Order>> GetOrdersByCustomerId(int customerid)
        {
            return await _context.Orders.Include(x => x.OrderItems).Where(x => x.CustomerId == customerid).ToListAsync();
        }

        public async Task<IEnumerable<Core.Entities.Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            var orders = await _context.Orders
                .Include(x => x.OrderItems)
                .Where(x => x.OrderDate >= startDate && x.OrderDate <= endDate)
                .ToListAsync();
            return orders;
        }

        public async Task<bool> UpdateOrder(Core.Entities.Order order)
        {
            var orderExsit = _context.Orders.Find(order.Id);
            if (orderExsit == null)
            {
                throw new ArgumentNullException();
            }
            _context.Entry(orderExsit).CurrentValues.SetValues(order);
            return await _context.SaveChangesAsync() > 0;
            
        }

        public async Task<bool> UpdateOrderStatus(Guid orderId, string newStatus)
        {
            var order = _context.Orders.Find(orderId);
            if (order == null)
            {
                throw new ArgumentNullException();
            }
            order.Status = newStatus;
            _context.Orders.Update(order);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
