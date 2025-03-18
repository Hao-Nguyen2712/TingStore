using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

using Microsoft.Extensions.Logging;

namespace TingStore.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product/[action]/{id?}")]
    public class ManagementProductController : Controller
    {
        private readonly ILogger<ManagementProductController> _logger;
        private readonly HttpClient client = null;
        private string producApi;

        public ManagementProductController(ILogger<ManagementProductController> logger)
        {
            _logger = logger;
              client = new HttpClient(); // cấp bộ nhớ khởi tạo client
            var contentType = new MediaTypeWithQualityHeaderValue("application/json"); // dạng json
            client.DefaultRequestHeaders.Accept.Add(contentType); // client chỉ chắp nhận những object dạng json
            this.producApi = "http://localhost:5001/apigateway/product/GetAllProducts";
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
