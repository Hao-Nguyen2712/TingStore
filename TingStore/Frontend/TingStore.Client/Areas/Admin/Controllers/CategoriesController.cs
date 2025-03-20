// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TingStore.Client.Areas.Admin.Models.Categories;
using TingStore.Client.Areas.Admin.Services.Categories;
using TingStore.Client.Areas.User.Models.Categories;

namespace TingStore.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ManagementCategory/[action]/{id?}")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> ListCategory()
        {
            var category = await _categoryService.GetAllCategories();
            ViewBag.TotalCategories = await _categoryService.GetCategoriesCount();
            ViewBag.ActiveCategories = await _categoryService.GetAllActiveCategoriesCount();
            ViewBag.InactiveCategories = await _categoryService.GetAllInactiveCategoriesCount();
            return View(category);
        }
        public async Task<IActionResult> Inactive()
        {
            var usersInactive = await _categoryService.GetAllInactiveCategories();
            ViewBag.TotalCategories = await _categoryService.GetCategoriesCount();
            ViewBag.ActiveCategories = await _categoryService.GetAllActiveCategoriesCount();
            ViewBag.InactiveCategories = await _categoryService.GetAllInactiveCategoriesCount();
            return View("ListCategory", usersInactive);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryRequest category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            var existingCategory = await _categoryService.GetCategoryByNameInactive(category.Name);

            if (existingCategory != null)
            {
                ViewBag.RestoreMessage = "Category already exists. Do you want to restore it?";
                ViewBag.RestoreCategoryId = existingCategory.Id;
                return View(category);
            }
            try
            {
                await _categoryService.CreateCategory(category);
                return RedirectToAction("ListCategory");
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error creating category: {ex.Message}";
                return View(category);
            }
        }
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var category = await _categoryService.GetCategoryById(id);
                if (category == null)
                {
                    return RedirectToAction("Index");
                }
                var updateCategoryRequest = new UpdateCategoryRequest
                {
                    Id = category.Id,
                    Name = category.Name
                };
                return View(updateCategoryRequest);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error fetching categories: {ex.Message}";
                return RedirectToAction("ListCategory");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UpdateCategoryRequest categoryRequest)
        {
            if(id != categoryRequest.Id)
            {
                return NotFound();
            }
            try
            {
                await _categoryService.UpdateCategory(categoryRequest);
                return RedirectToAction("ListCategory");
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error fetching categories: {ex.Message}";
                return View(categoryRequest);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _categoryService.DeleteCategory(id);
                return Json(new { success = true, message = "Category deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error delete category: {ex.Message}" });
            }
        }
        //public async Task<IActionResult> Restore(string id)
        //{
        //    var user = await _categoryService.GetCategoryById(id);
        //    return View(user);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmed(string id)
        {
            bool result = await _categoryService.RestoreCategory(id);
            
            if (!result)
            {
                return Json(new { success = false, message = "Failed to restore category!" });
            }
            
            return Json(new { success = true, message = "Category restored successfully!", categoryId = id });
        }
    }
}
