using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

using Microsoft.Extensions.Logging;
using TingStore.Client.Areas.Admin.Services.ProductManagement;
using TingStore.Client.Areas.Admin.Models.Products;
using System.Text.Json;

namespace TingStore.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ManagementProduct/[action]/{id?}")]
    public class ManagementProductController : Controller
    {
        private readonly ILogger<ManagementProductController> _logger;
        private readonly IProductManagementService _productManagementService;

        public ManagementProductController(ILogger<ManagementProductController> logger, IProductManagementService productManagementService)
        {
            _logger = logger;
            this._productManagementService = productManagementService;
        }

        public async Task<IActionResult> Index(int? indexpag, string? sort)
        {
            int indexpage = indexpag ?? 1;
            _logger.LogInformation($"Page Index: {indexpage}");
            var ProductList = await _productManagementService.GetAllProducts(indexpage, sort);
            int numberOfPage = (int)Math.Ceiling((double)ProductList.Count / ProductList.PageSize);
            ViewBag.numberOfPage = numberOfPage;
            if (sort == "priceAsc")
            {
                ViewBag.sortPage = "priceAsc";
            }
            else if (sort == "priceDesc")
            {
                ViewBag.sortPage = "priceDesc";
            }
            else
            {
                ViewBag.sortPage = "default";
            }
            return View(ProductList);
        }

        public async Task<IActionResult> ProductDetail(string id)
        {
            var product = await this._productManagementService.GetProductById(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductImage(string id, List<IFormFile> formFiles)
        {

            if (formFiles == null || formFiles.Count == 0)
            {
                ModelState.AddModelError("", "Please select at least one image.");
                return Error();
            }
            System.Console.WriteLine("__________________________");
            System.Console.WriteLine("id product: " + id);
            try
            {
                var addImageRespone = await this._productManagementService.AddProductImage(id, formFiles);
                return RedirectToAction("ProductDetail", new { id = id });
            }
            catch (System.Exception)
            {
                return RedirectToAction("ProductDetail", new { id = id });
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductInput productInput)
        {
            System.Console.WriteLine(productInput.ProductFile.Count);
            ProductDescription productDescription = new ProductDescription
            {
                Brand = productInput.Brand,
                Model = productInput.Model,
                ReleaseDate = productInput.Relaease,
                Type = productInput.type,
                Size = productInput.Size,
                Resolution = productInput.Resolution,
                Dimensions = productInput.Dimensions,
                Weight = productInput.Weight,
                SIM = productInput.Sim,
                OS = productInput.Os,
                Chipset = productInput.Chipset,
                ReslaeaseDate = productInput.ReslaeaseDate,
                Description = productInput.Description
            };
            string jsonDescription = JsonSerializer.Serialize(productDescription, new JsonSerializerOptions { WriteIndented = true });
            System.Console.WriteLine("json description: " + jsonDescription);
            System.Console.WriteLine(productInput.ProductFile[0].FileName);
            ProductResquest productResquest = new ProductResquest
            {
                Name = productInput.Name,
                ProductFile = productInput.ProductFile[0],
                Description = jsonDescription.ToString(),
                Price = productInput.Price,
                Stock = productInput.Stock,
                CategoryId = productInput.Category,
                IsActive = true
            };
            System.Console.WriteLine("------------------------------------------------");
            System.Console.WriteLine(productResquest.ToString());
            try
            {
                var productRespone = await this._productManagementService.CreateProduct(productResquest);
                var addImageRespone = await this._productManagementService.AddProductImage(productRespone.Id, productInput.ProductFile);
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                return View(productInput);
            }
        }

        public async Task<IActionResult> UpdateProduct(string id)
        {
            var product = await this._productManagementService.GetProductById(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            ProductDescription productDescription = JsonSerializer.Deserialize<ProductDescription>(product.Description);
            UpdateProductInput updateProductInput = new UpdateProductInput
            {
                Id = product.Id,
                Name = product.Name,
                Brand = productDescription.Brand,
                Model = productDescription.Model,
                Relaease = productDescription.ReleaseDate,
                type = productDescription.Type,
                Size = productDescription.Size,
                Resolution = productDescription.Resolution,
                Dimensions = productDescription.Dimensions,
                Weight = productDescription.Weight,
                Sim = productDescription.SIM,
                Os = productDescription.OS,
                Chipset = productDescription.Chipset,
                ReslaeaseDate = productDescription.ReslaeaseDate,
                Description = productDescription.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                IsActive = product.IsActive
            };
            return View(updateProductInput);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductInput updateProductInput)
        {
            ProductDescription productDescription = new ProductDescription
            {
                Brand = updateProductInput.Brand,
                Model = updateProductInput.Model,
                ReleaseDate = updateProductInput.Relaease,
                Type = updateProductInput.type,
                Size = updateProductInput.Size,
                Resolution = updateProductInput.Resolution,
                Dimensions = updateProductInput.Dimensions,
                Weight = updateProductInput.Weight,
                SIM = updateProductInput.Sim,
                OS = updateProductInput.Os,
                Chipset = updateProductInput.Chipset,
                ReslaeaseDate = updateProductInput.ReslaeaseDate,
                Description = updateProductInput.Description
            };
            string jsonDescription = JsonSerializer.Serialize(productDescription, new JsonSerializerOptions { WriteIndented = true });
            System.Console.WriteLine("json description: " + jsonDescription);

            UpdateProductResquest updateProductResquest = new UpdateProductResquest
            {
                Id = updateProductInput.Id,
                Name = updateProductInput.Name,
                Description = jsonDescription.ToString(),
                Price = updateProductInput.Price,
                Stock = updateProductInput.Stock,
                CategoryId = updateProductInput.CategoryId,
                IsActive = true
            };
            try
            {
                var respone = await this._productManagementService.UpdateProduct(updateProductResquest);
                return RedirectToAction("ProductDetail", new { id = updateProductResquest.Id });
            }
            catch (System.Exception)
            {
                return View();
            }

        }

        public async Task<IActionResult> DeleteProduct(string id)
        {
            try
            {
                await this._productManagementService.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                return Error();
            }
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
