using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Order.Core.Entities;
using Order.Core.Repositories;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly OrderDbContext _context;
        public OrderItemRepository(OrderDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteOrderItem(int orderItemId)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemId);
            if (orderItem == null)
            {
               return false;
            }
            _context.OrderItems.Remove(orderItem);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<int> GetTotalQuantitySoldByProductId(string productId)
        {
            var totalQuantity = await _context.OrderItems.Where(x => x.ProductId == productId).SumAsync(x => x.Quantity);
            return totalQuantity;
        }
        public async Task<bool> Update(OrderItem orderItem)
        {
            var orderItemExist = await _context.OrderItems.FindAsync(orderItem.Id);
            if (orderItemExist == null)
            {
                return false;
            }
            _context.Entry(orderItemExist).CurrentValues.SetValues(orderItem);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateOrderItemQuantity(int orderItemId, int newQuantity)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemId);
            if (orderItem == null)
            {
                return false;
            }
            orderItem.Quantity = newQuantity;
            _context.OrderItems.Update(orderItem);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
