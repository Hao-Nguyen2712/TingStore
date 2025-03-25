using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TingStore.Client.Areas.Admin.Models.Dashboard;
using TingStore.Client.Areas.Admin.Models.Order;
using TingStore.Client.Areas.Admin.Services.OrderMangement;
using TingStore.Client.Areas.Admin.Services.Users;

namespace TingStore.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    public class DashboardController : Controller
    {
        private readonly IOrderManagementService _orderManagementService;
        private readonly IUserService _userService;

        public DashboardController( IOrderManagementService orderManagementService, IUserService userService)
        {
            _orderManagementService = orderManagementService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {

            var model = new DashboardVM
            {
                TotalUsers = await _userService.GetAllUsersCount(),
                TotalOrders = await _orderManagementService.GetAllOrderCount(),
                Orders = await _orderManagementService.GetAllOrder()
            };
            
            return View(model);
        }
    }
}
