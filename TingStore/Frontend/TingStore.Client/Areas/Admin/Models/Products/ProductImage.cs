using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TingStore.Client.Areas.Admin.Models.Products
{
    public class ProductImage
    {
    public string ImageUrl { get; set; }
    public bool IsPrimary { get; set; }
    public DateTime CreateAt { get; set; }
    }
}
