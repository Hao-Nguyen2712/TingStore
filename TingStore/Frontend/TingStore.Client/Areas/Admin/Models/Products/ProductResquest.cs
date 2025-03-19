using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TingStore.Client.Areas.Admin.Models.Products
{
    public class ProductResquest
    {
        public string Name { get; set; }
        public IFormFile ProductFile { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string CategoryId { get; set; }
        public bool IsActive { get; set; }
        public override string ToString()
        {
            return $"Name: {Name}, " +
                   $"ProductFile: {(ProductFile != null ? ProductFile.FileName : "null")}, " +
                   $"Description: {Description}, " +
                   $"Price: {Price}, " +
                   $"Stock: {Stock}, " +
                   $"CategoryId: {CategoryId}, " +
                   $"IsActive: {IsActive}";
        }
    }
}
