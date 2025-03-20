// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TingStore.Client.Areas.Admin.Models.Categories;

namespace TingStore.Client.Areas.Admin.Services.Categories
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetAllCategories();
        Task<IEnumerable<CategoryResponse>> GetAllActiveCategories();
        Task<IEnumerable<CategoryResponse>> GetAllInactiveCategories();
        Task<CategoryResponse> GetCategoryById(string id);
        Task<IEnumerable<CategoryResponse>> GetCategoryByName(string name);
        Task<CategoryResponse> GetCategoryByNameInactive(string name);
        Task<CategoryResponse> CreateCategory(CreateCategoryRequest category);
        Task<bool> UpdateCategory(UpdateCategoryRequest category);
        Task<bool> DeleteCategory(string id);
        Task<bool> RestoreCategory(string id);
        Task<int> GetCategoriesCount();
        Task<int> GetAllActiveCategoriesCount();
        Task<int> GetAllInactiveCategoriesCount();
    }
}
