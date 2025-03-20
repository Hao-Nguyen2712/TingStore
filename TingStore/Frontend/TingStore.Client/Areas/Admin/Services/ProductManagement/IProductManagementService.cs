using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TingStore.Client.Areas.Admin.Models.Products;
using TingStore.Client.Areas.Admin.Models.Products.Specs;

namespace TingStore.Client.Areas.Admin.Services.ProductManagement
{
    public interface IProductManagementService
    {
        Task<Pagination<ProductResponse>> GetAllProducts(int indexPage, string sort, string cateName);
        Task<ProductResponse> GetProductById(string? id);

        Task<ProductResponse> CreateProduct(ProductResquest productResquest);

        Task<bool> AddProductImage(string id,List<IFormFile> files);

        Task<bool> UpdateProduct(UpdateProductResquest updateProduct);
        Task<bool> DeleteProduct(string id);
        Task<IEnumerable<ProductResponse>> GetAllProductNoFilter();

    }
}
