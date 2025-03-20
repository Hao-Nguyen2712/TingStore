// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Category.Core.Specs;

namespace Category.Core.Repositories
{
    public interface ICategoryRepository
    {
        Task<Pagination<Category.Core.Entities.Category>> GetCategories(CategorySpecParams categorySpecParams);
        Task<IEnumerable<Category.Core.Entities.Category>> GetAllCategories();
        Task<IEnumerable<Category.Core.Entities.Category>> GetAllActiveCategories();
        Task<IEnumerable<Category.Core.Entities.Category>> GetAllInactiveCategories();
        Task<Category.Core.Entities.Category> GetCategoryById(string id);
        Task<IEnumerable<Category.Core.Entities.Category>> GetCategoryByName(string name);
        Task<Category.Core.Entities.Category> GetCategoryByNameInactive(string name);
        Task<Category.Core.Entities.Category> CreateCategory(Category.Core.Entities.Category category);
        Task<bool> UpdateCategory(Category.Core.Entities.Category category);
        Task<bool> DeleteCategory(string id);
        Task<bool> RestoreCategory(string id);
        Task<int> GetCategoriesCount();
        Task<int> GetAllActiveCategoriesCount();
        Task<int> GetAllInactiveCategoriesCount();

    }
}
