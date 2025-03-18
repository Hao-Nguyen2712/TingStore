// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Specs;
using TingStore.Client.Areas.User.Models.Cart;
using TingStore.Client.Areas.User.Models.Products;
using TingStore.Client.Areas.User.Services.Cart;
using TingStore.Client.Areas.User.Services.Products;

namespace TingStore.Client.Areas.User.Controllers
{
    [Area("User")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public ProductController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
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

        // add to cart
        public async Task<IActionResult> AddCart(CartItem item)
        {
            item.Quantity = 1; // mặc định với cart là 1
            var idUser = 1; // Lấy bằng context sau khi đã đăng nhập

            CartRequest cartRequest = new()
            {
                Id = idUser,
                Items = new List<CartItem> { item }
            };
            var result = await _cartService.AddToCart(cartRequest);
            if (result)
            {
                TempData["SuccessMessage"] = "Add to cart successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Add to cart failed";
            }
            return RedirectToAction("Shop");
        }
    }


}
