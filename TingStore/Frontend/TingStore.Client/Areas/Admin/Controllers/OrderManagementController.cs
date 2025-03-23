using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TingStore.Client.Areas.Admin.Services.OrderMangement;
using TingStore.Client.Areas.Admin.Services.ProductManagement;
using TingStore.Client.Areas.Admin.Services.Users;

namespace TingStore.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/OrderManagement/[action]/{id?}")]
    public class OrderManagementController : Controller
    {
        private readonly ILogger<OrderManagementController> _logger;
        private readonly IOrderManagementService _orderManagementService;
        private readonly IProductManagementService _productManagementService;

        private readonly IUserService _userService;

        public OrderManagementController(ILogger<OrderManagementController> logger, IOrderManagementService orderManagementService, IUserService userService, IProductManagementService productManagementService)
        {
            _logger = logger;
            this._orderManagementService = orderManagementService;
            this._userService = userService;
            this._productManagementService = productManagementService;
        }

        public async Task<IActionResult> Index()
        {
            var listOrder = await this._orderManagementService.GetAllOrder();
            var listUser = await this._userService.GetAllUsers();
            foreach (var order in listOrder)
            {
                order.userResponse = listUser.FirstOrDefault(user => user.Id == order.CustomerId);
            }

            return View(listOrder);
        }

        public async Task<IActionResult> OrderDetail(string id)
        {
            var orderDetail = await this._orderManagementService.GetOrderByID(id);
            var listUser = await this._userService.GetAllUsers();
            var LisrProduct = await this._productManagementService.GetAllProductNoFilter();
            orderDetail.userResponse = listUser.FirstOrDefault(p => p.Id == orderDetail.CustomerId);
            foreach(var itemProduct in orderDetail.Items) {
                itemProduct.productResponse = LisrProduct.FirstOrDefault(p => p.Id.Equals(itemProduct.ProductId));
            }
            return View(orderDetail);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
