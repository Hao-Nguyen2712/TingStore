using Microsoft.AspNetCore.Mvc;

namespace TingStore.Client.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public OrderController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
