// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Specs;
using TingStore.Client.Areas.User.Models.Products;
using TingStore.Client.Areas.User.Services.Products;

namespace TingStore.Client.Areas.User.Controllers
{
    [Area("User")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Shop(int pageIndex = 1, int pageSize = 10, string brandId = null, string sort = null, string search = null)
        {
            try
            {
                var productSpecParams = new ProductSpecParams
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    BrandId = brandId,
                    Sort = sort,
                    Search = search
                };
                var productList = await _productService.GetAllProducts(productSpecParams);
                return View(productList);
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = $"Error fetching products: {ex.Message}";
                return View(new Pagination<ProductResponse>());
            }
        }


        public async Task<IActionResult> Details(string id)
        {
            var product = await _productService.GetProductById(id);
            return View(product);
        }
    }


}
