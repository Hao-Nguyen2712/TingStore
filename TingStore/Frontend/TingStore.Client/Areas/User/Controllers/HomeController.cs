// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using TingStore.Client.Areas.User.Models.Authen;
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
            foreach (var category in categories)
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

        public IActionResult Login()
        {

            return View(new AuthenRequest());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthenRequest request)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Dữ liệu nhập không hợp lệ!";
                return View(request);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5188/");
                var response = await client.PostAsJsonAsync("api/auth/login", request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        var token = JsonConvert.DeserializeObject<AuthenticationResponse>(result);
                        if (token == null)
                        {
                            ViewBag.Error = "Login Fail";
                            return View(request);
                        }
                        var cookieOptions = new CookieOptions
                        {
                            HttpOnly = false,
                            Secure = true,
                            Expires = DateTime.UtcNow.AddDays(1),
                            SameSite = SameSiteMode.None,
                        };

                        var cookieOptions2 = new CookieOptions
                        {
                            HttpOnly = false,
                            Secure = true,
                            Expires = DateTime.UtcNow.AddMinutes(15),
                            SameSite = SameSiteMode.None,
                        };

                        Response.Cookies.Append("acessToken", token.Token.AccessToken, cookieOptions2);
                        Response.Cookies.Append("refreshToken", token.Token.RefreshToken, cookieOptions);
                        var emailPrefix = token.Email.Split('@')[0];
                        Response.Cookies.Append("email", emailPrefix, cookieOptions);

                        return RedirectToAction("Index", "Home", new { area = "User" });
                    }
                }

                ViewBag.Error = "Login Fail";
            }

            return View(request);
        }
                
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            using (var client = new HttpClient())
            {
                var model = new AuthenRequest
                {
                    Email = registerRequest.Email,
                    Password = registerRequest.Password
                };
                client.BaseAddress = new Uri("http://localhost:5188/");
                var response = await client.PostAsJsonAsync("api/auth/register", model);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Success = "Đăng Kí Thành Công!";
                    return RedirectToAction("Login", "Home" , new {area = "User"});
                }
                else
                {
                    ViewBag.Error= "Register failed";
                    return View(registerRequest);
                }
            }
        
        }

        public async Task<IActionResult> LogoutAsync()
        {
            using (var client = new HttpClient())
            {
                var accessToken = Request.Cookies["acessToken"];
                var refreshToken = Request.Cookies["refreshToken"];
                var token = new TokenModel
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };
                client.BaseAddress = new Uri("http://localhost:5188/");
                var response = await client.PostAsJsonAsync("api/auth/logout", token);

                Response.Cookies.Delete("acessToken");
                Response.Cookies.Delete("refreshToken");
                Response.Cookies.Delete("email");
                return RedirectToAction("Index", "Home", new { area = "User" });
            }
        }


        public IActionResult Register()
        {
            return View();
        }

       

        public IActionResult ForgetPassword()
        {
            return View();
        }
    }
}
