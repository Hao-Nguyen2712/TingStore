using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TingStore.Client.Areas.User.Controllers
{
    [Area("User")]
    [Route("[area]/[controller]/[action]")]
    public class DasboardController : Controller
    {
        private readonly ILogger<DasboardController> _logger;

        public DasboardController(ILogger<DasboardController> logger)
        {
            _logger = logger;
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ContactUs()
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
