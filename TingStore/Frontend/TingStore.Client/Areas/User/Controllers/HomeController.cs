// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Mvc;
using TingStore.Client.Areas.User.Models.Home;
using TingStore.Client.Areas.User.Services.Categories;
using TingStore.Client.Areas.User.Services.Products;
using TingStore.Client.Areas.User.Services.Reviews;

namespace TingStore.Client.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ICategoryUserService _categoryUserService;
        private readonly IProductService _productService;
        private readonly IReviewProductService _reviewProductService;

        public HomeController(ICategoryUserService categoryUserService, IProductService productService, IReviewProductService reviewProductService = null)
        {
            _categoryUserService = categoryUserService;
            _productService = productService;
            _reviewProductService = reviewProductService;
        }

        public async Task<IActionResult> Index()
        {
            // Get list Categories
            var categories = await _categoryUserService.GetCategories();
            // Get list Products
            var products = await _productService.GetAllProducts(new Product.Core.Specs.ProductSpecParams());
            var productList = products.Data.ToList();

            // Count product in category
            foreach(var category in categories)
            {
                var count = productList.Count(p => p.CategoryId == category.Name);
                category.ProductCount = count;
            }
            var model = new HomeViewModel
            {
                Categories = (List<Models.Categories.CategoryResponse>)categories,
                Products = productList // gáng danh sách products vào Products
            };

            // fetch rate and count review
            var averageRatings = new Dictionary<string, double>();
            var reviewCounts = new Dictionary<string, int>();

            foreach (var product in products.Data)
            {
                var averageRating = await _reviewProductService.GetAverageRatingByProductId(product.Id);
                var reviewCount = await _reviewProductService.GetReviewCountByProductId(product.Id);

                averageRatings[product.Id] = averageRating;
                reviewCounts[product.Id] = reviewCount;
            }

            ViewBag.AverageRatings = averageRatings;
            ViewBag.ReviewCounts = reviewCounts;
            return View(model);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }
    }
}
