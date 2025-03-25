using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TingStore.Client.Areas.Admin.Models.Products;

namespace TingStore.Client.Areas.Admin.Models.Order
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public ProductResponse productResponse { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid OrderId { get; set; }
    }
}
