// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var filter = builder.Empty;
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
                    Count = await _categoryContext.Categories.CountDocumentsAsync(p => true)
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
                Count = await _categoryContext.Categories.CountDocumentsAsync(p => true)
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
            return await _categoryContext.Categories.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Core.Entities.Category>> GetCategoryByName(string name)
        {
            FilterDefinition<Core.Entities.Category> filter = Builders<Core.Entities.Category>.Filter.Eq(p => p.Name, name);
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
            FilterDefinition<Core.Entities.Category> filter = Builders<Core.Entities.Category>.Filter.Eq(c => c.Id, id);
            DeleteResult deleteResult = await _categoryContext.Categories.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }  
    }
}
