// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Category.Core.Repositories;
using Category.Core.Specs;
using Category.Infrastructure.Data;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Category.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ICategoryContext _categoryContext;
        public CategoryRepository(ICategoryContext categoryContext)
        {
            this._categoryContext = categoryContext;
        }

        public async Task<Pagination<Core.Entities.Category>> GetCategories(CategorySpecParams categorySpecParams)
        {
            var builder = Builders<Core.Entities.Category>.Filter;
            var filter = builder.Eq(c => c.IsActive, true);
            if (!string.IsNullOrEmpty(categorySpecParams.Search))
            {
                var searchFilter = builder.Regex(x => x.Name, new BsonRegularExpression(categorySpecParams.Search));
                filter &= searchFilter;
            }
            if (!string.IsNullOrEmpty(categorySpecParams.TypeId))
            {
                var typeFilter = builder.Eq(x => x.Id, categorySpecParams.TypeId);
                filter &= typeFilter;
            }
            if (!string.IsNullOrEmpty(categorySpecParams.Sort))
            {
                return new Pagination<Core.Entities.Category>
                {
                    PageSize = categorySpecParams.PageSize,
                    PageIndex = categorySpecParams.PageIndex,
                    Data = await DataFilter(categorySpecParams, filter),
                    Count = await _categoryContext.Categories.CountDocumentsAsync(filter)
                };
            }
            return new Pagination<Core.Entities.Category>
            {
                PageSize = categorySpecParams.PageSize,
                PageIndex = categorySpecParams.PageIndex,
                Data = await _categoryContext.Categories
                .Find(filter)
                .Sort(Builders<Core.Entities.Category>.Sort.Ascending("Name"))
                .Skip(categorySpecParams.PageSize * (categorySpecParams.PageIndex - 1))
                .Limit(categorySpecParams.PageSize)
                .ToListAsync(),
                Count = await _categoryContext.Categories.CountDocumentsAsync(filter)
            };
        }

        private async Task<IReadOnlyList<Core.Entities.Category>> DataFilter(CategorySpecParams categorySpecParams, FilterDefinition<Core.Entities.Category> filter)
        {
            switch(categorySpecParams.Sort)
            {
                case "nameAsc":
                    return await _categoryContext.Categories
                                .Find(filter)
                                .Sort(Builders<Core.Entities.Category>.Sort.Ascending("Name"))
                                .Skip(categorySpecParams.PageSize * (categorySpecParams.PageIndex - 1))
                                .Limit(categorySpecParams.PageSize)
                                .ToListAsync();
                case "nameDesc":
                    return await _categoryContext.Categories
                                .Find(filter)
                                .Sort(Builders<Core.Entities.Category>.Sort.Descending("Name"))
                                .Skip(categorySpecParams.PageSize * (categorySpecParams.PageIndex - 1))
                                .Limit(categorySpecParams.PageSize)
                                .ToListAsync();
                default:
                    return await _categoryContext.Categories
                                .Find(filter)
                                .Sort(Builders<Core.Entities.Category>.Sort.Ascending("Name"))
                                .Skip(categorySpecParams.PageSize * (categorySpecParams.PageIndex - 1))
                                .Limit(categorySpecParams.PageSize)
                                .ToListAsync();
            }
        }

        public async Task<Core.Entities.Category> GetCategoryById(string id)
        {
            return await _categoryContext.Categories.Find(c => c.Id == id && c.IsActive).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Core.Entities.Category>> GetAllCategories()
        {
            try
            {
                var filter = Builders<Core.Entities.Category>.Filter.Eq(c => c.IsActive, true);
                var categories = await _categoryContext.Categories.Find(filter).ToListAsync();

                if (categories == null || !categories.Any())
                {
                    Console.WriteLine("Không có danh mục nào được tìm thấy.");
                }

                return categories;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy danh mục: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Core.Entities.Category>> GetCategoryByName(string name)
        {
            var filter = Builders<Core.Entities.Category>.Filter.Eq(c => c.Name, name) &
                 Builders<Core.Entities.Category>.Filter.Eq(c => c.IsActive, true);
            return await _categoryContext.Categories.Find(filter).ToListAsync();
        }

        public async Task<Core.Entities.Category> CreateCategory(Core.Entities.Category category)
        {
            await _categoryContext.Categories.InsertOneAsync(category);
            return category;
        }
        public async Task<bool> UpdateCategory(Core.Entities.Category category)
        {
            var updateCategory = await _categoryContext.Categories.ReplaceOneAsync(p => p.Id == category.Id, category);
            return updateCategory.IsAcknowledged && updateCategory.ModifiedCount > 0;
        }
        
        public async Task<bool> DeleteCategory(string id)
        {
            var filter = Builders<Core.Entities.Category>.Filter.Eq(c => c.Id, id);
            var update = Builders<Core.Entities.Category>.Update.Set(c => c.IsActive, false);
            var deleteResult = await _categoryContext.Categories.UpdateOneAsync(filter, update);
            return deleteResult.IsAcknowledged && deleteResult.ModifiedCount > 0;
        }

        public async Task<IEnumerable<Core.Entities.Category>> GetAllActiveCategories()
        {
            var filter = Builders<Core.Entities.Category>.Filter.Eq(c => c.IsActive, true);
            return await _categoryContext.Categories.Find(filter).ToListAsync();
        }
        public async Task<IEnumerable<Core.Entities.Category>> GetAllInactiveCategories()
        {
            var filter = Builders<Core.Entities.Category>.Filter.Eq(c => c.IsActive, false);
            return await _categoryContext.Categories.Find(filter).ToListAsync();
        }
        public async Task<bool> RestoreCategory(string id)
        {
            var filter = Builders<Core.Entities.Category>.Filter.Eq(c => c.Id, id);
            var update = Builders<Core.Entities.Category>.Update.Set(c => c.IsActive, true);
            var updateResult = await _categoryContext.Categories.UpdateOneAsync(filter, update);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
        public async Task<int> GetCategoriesCount()
        {
            return (int)await _categoryContext.Categories.CountDocumentsAsync(FilterDefinition<Core.Entities.Category>.Empty) ;
        }
        public async Task<int> GetAllActiveCategoriesCount()
        {
            var filter = Builders<Core.Entities.Category>.Filter.Eq(c => c.IsActive, true);
            return (int)await _categoryContext.Categories.CountDocumentsAsync(filter);
        }
        public async Task<int> GetAllInactiveCategoriesCount()
        {
            var filter = Builders<Core.Entities.Category>.Filter.Eq(c => c.IsActive, false);
            return (int)await _categoryContext.Categories.CountDocumentsAsync(filter);
        }

        public async Task<Core.Entities.Category> GetCategoryByNameInactive(string name)
        {
            var filter = Builders<Core.Entities.Category>.Filter.Eq(c => c.Name, name) &
                 Builders<Core.Entities.Category>.Filter.Eq(c => c.IsActive, false);
            return await _categoryContext.Categories.Find(filter).FirstOrDefaultAsync();
        }
    }
}
