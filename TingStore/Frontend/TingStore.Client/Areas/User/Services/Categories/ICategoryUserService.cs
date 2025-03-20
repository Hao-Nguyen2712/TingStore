// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using TingStore.Client.Areas.User.Models.Categories;

namespace TingStore.Client.Areas.User.Services.Categories
{
    public interface ICategoryUserService
    {
        Task<IEnumerable<CategoryResponse>> GetCategories();
        Task<CategoryResponse> GetCategoryById(string id);
        Task<CategoryResponse> GetCategoryByName(string name);
    }
}
