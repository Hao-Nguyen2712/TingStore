// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Drawing.Printing;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Specs;
using TingStore.Client.Areas.User.Models.Cart;
using TingStore.Client.Areas.User.Models.Products;
using TingStore.Client.Areas.User.Services.Cart;
using TingStore.Client.Areas.User.Services.Categories;
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
        private readonly ICategoryUserService _categoryService;

        public ProductController(IProductService productService, IReviewProductService reviewProductService, ICartService cartService, ICategoryUserService categoryService)
        {
            _productService = productService;
            _reviewProductService = reviewProductService;
            _cartService = cartService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Shop(int pageIndex = 1, int pageSize = 12, string brandId = null, string sort = null, string search = null)
        {
            try
            {
                // Tạo ProductSpecParams để lấy tổng số sản phẩm (không giới hạn PageSize)
                var countSpecParams = new   ProductSpecParams        
                {
                    PageIndex = 1,
                    PageSize = int.MaxValue,
                    BrandId = brandId,
                    Sort = sort,
                    Search = search
                };

                // Lấy tổng số sản phẩm thực tế
                var allProducts = await _productService.GetAllProducts(countSpecParams);
                var totalItems = allProducts?.Data?.Count() ?? 0;
                var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

                // Điều chỉnh pageIndex
                if (pageIndex < 1) pageIndex = 1;
                if (pageIndex > totalPages && totalPages > 0) pageIndex = totalPages;
                else if (totalPages == 0) pageIndex = 1;

                // Tạo ProductSpecParams cho phân trang
                var productSpecParams = new ProductSpecParams
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    BrandId = brandId,
                    Sort = sort,
                    Search = search
                };

                // Lấy danh sách sản phẩm phân trang
                var productList = await _productService.GetAllProducts(productSpecParams);

                // Kiểm tra null cho productList.Data
                if (productList?.Data == null)
                {
                    ViewBag.Error = "No products found.";
                    return View(new Pagination<ProductResponse>());
                }

                // AverageRatings
                var averageRatings = new Dictionary<string, double>();
                foreach (var product in productList.Data)
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
                ViewBag.ReviewCounts = reviewCounts;

                // Các thông tin khác cho view
                var filterParams = new Dictionary<string, string>
        {
            { "brandId", brandId ?? "" }, // Sửa để tránh null
            { "sort", sort ?? "" },
            { "search", search ?? "" }
        };

                var categoryList = await _categoryService.GeAllActiveCategoriesListString();
                ViewBag.categoryList = categoryList;
                ViewBag.FilterParams = filterParams;
                ViewBag.BrandId = brandId;
                ViewBag.Sort = sort;
                ViewBag.Search = search;
                ViewBag.PageIndex = pageIndex;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalItems = totalItems;
                ViewBag.TotalPages = totalPages;

                if (brandId != null)
                {
                    ViewBag.categorySelected = brandId;
                }

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

                // Lấy brand từ Description
                var productDescription = JsonSerializer.Deserialize<ProductDescription>(product.Description);
                string currentBrand = productDescription?.Brand;

                // Lấy tất cả sản phẩm (giới hạn số lượng lớn hơn một chút để lọc)
                var specParams = new ProductSpecParams
                {
                    PageSize = 10 // Lấy 10 sản phẩm để có đủ dữ liệu lọc
                };
                var allProducts = await _productService.GetAllProducts(specParams);

                // Lọc sản phẩm cùng hãng (loại trừ sản phẩm hiện tại)
                var sameBrandProducts = allProducts.Data
                    .Where(p => JsonSerializer.Deserialize<ProductDescription>(p.Description)?.Brand == currentBrand && p.Id != id)
                    .Take(6)
                    .ToList();

                // Lọc sản phẩm khác hãng
                var differentBrandProducts = allProducts.Data
                    .Where(p => JsonSerializer.Deserialize<ProductDescription>(p.Description)?.Brand != currentBrand)
                    .Take(6)
                    .ToList();

                // Tính toán AverageRating và ReviewCount cho từng sản phẩm
                foreach (var prod in sameBrandProducts)
                {
                    prod.AverageRating = await _reviewProductService.GetAverageRatingByProductId(prod.Id);
                    prod.ReviewCount = await _reviewProductService.GetReviewCountByProductId(prod.Id);
                }

                foreach (var prod in differentBrandProducts)
                {
                    prod.AverageRating = await _reviewProductService.GetAverageRatingByProductId(prod.Id);
                    prod.ReviewCount = await _reviewProductService.GetReviewCountByProductId(prod.Id);
                }

                ViewBag.SameBrandRecommendations = sameBrandProducts;
                ViewBag.DifferentBrandRecommendations = differentBrandProducts;

                return View(product);
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = $"Error fetching product details: {ex.Message}";
                return View(new ProductResponse());
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] CartItem item)
        {
            item.Quantity = 1;
            var idUser = 1; // Lấy từ context sau khi đã đăng nhập

            CartRequest cartRequest = new()
            {
                Id = idUser,
                Items = new List<CartItem> { item }
            };

            var result = await _cartService.AddToCart(cartRequest);

            return Json(new
            {
                success = result,
                message = result ? "Add to cart successfully" : "Add to cart failed"
            });
        }


        [HttpPost]
        public async Task<IActionResult> SeachProduct(string ProductName)
        {
            return RedirectToAction("Shop", new { search = ProductName });
        }

        [HttpPost]
        public async Task<IActionResult> FilterByCategory(string CategoryName)
        {
            return RedirectToAction("Shop", new { brandId = CategoryName });
        }
    }
}
