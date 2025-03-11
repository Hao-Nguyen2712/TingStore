using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order.Core.Entities;

namespace Order.Core.Repositories
{
    public interface IOrderItemRepository
    {
        public Task<bool> Update(OrderItem orderItem);
        public Task<bool> DeleteOrderItem(Guid orderItemId);
        Task<bool> UpdateOrderItemQuantity(Guid orderItemId, int newQuantity);
        Task<int> GetTotalQuantitySoldByProductId(string productId);


    }
}
