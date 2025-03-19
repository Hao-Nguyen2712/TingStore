// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Specs;
using TingStore.Client.Areas.User.Models.Cart;
using TingStore.Client.Areas.User.Models.Products;
using TingStore.Client.Areas.User.Services.Cart;
using TingStore.Client.Areas.User.Services.Products;
using TingStore.Client.Areas.User.Services.Reviews;

namespace TingStore.Client.Areas.User.Controllers
{
    [Area("User")]
    [Route("[area]/[controller]/[action]/{id?}")]
    [Route("[area]/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IReviewProductService _reviewProductService;
        private readonly ICartService _cartService;

        public ProductController(IProductService productService, IReviewProductService reviewProductService, ICartService cartService)
        {
            _productService = productService;
            _reviewProductService = reviewProductService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Shop(int pageIndex = 1, int pageSize = 10, string brandId = null, string sort = null, string search = null)
        {
            try
            {
                // Tạo ProductSpecParams
                var productSpecParams = new ProductSpecParams
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    BrandId = brandId,
                    Sort = sort,
                    Search = search
                };

                var productList = await _productService.GetAllProducts(productSpecParams);

                var totalItems = productList.Count;
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                if (pageIndex < 1) pageIndex = 1;
                if (pageIndex > totalPages && totalPages > 0) pageIndex = totalPages;
                else if (totalPages == 0) pageIndex = 1;

                productSpecParams.PageIndex = pageIndex;
                productList = await _productService.GetAllProducts(productSpecParams);

                // averageRatings
                var averageRatings = new Dictionary<string, double>();
                foreach ( var product in productList.Data)
                {
                    var averageRating = await _reviewProductService.GetAverageRatingByProductId(product.Id);
                    averageRatings[product.Id] = averageRating;
                }
                ViewBag.AverageRatings = averageRatings;

                // Review Count
                var reviewCounts = new Dictionary<string, int>();
                foreach (var product in productList.Data)
                {
                    var reviewCount = await _reviewProductService.GetReviewCountByProductId(product.Id);
                    reviewCounts[product.Id] = reviewCount;
                }
                ViewBag.ReviewCounts     = reviewCounts;

                var filterParams = new Dictionary<string, string>
                {
                    { "brandId", brandId },
                    { "sort", sort },
                    { "search", search }
                };

                ViewBag.FilterParams = filterParams;
                ViewBag.BrandId = brandId;
                ViewBag.Sort = sort;
                ViewBag.Search = search;

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
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                var product = await _productService.GetProductById(id);
                if (product == null)
                {
                    return NotFound();
                }

                // Lấy danh sách review và gán vào thuộc tính Reviews
                product.Reviews = (await _reviewProductService.GetReviewsByProductId(id)).ToList();
                var averageRating = await _reviewProductService.GetAverageRatingByProductId(id);
                ViewBag.AverageRating = averageRating;
                return View(product);
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = $"Error fetching product details: {ex.Message}";
                return View(new ProductResponse());
            }
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
