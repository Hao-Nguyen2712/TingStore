// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Category.Application.Commands;
using Category.Application.Mappers;
using Category.Application.Responses;
using Category.Core.Repositories;
using MediatR;

namespace Category.Application.Handlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CreateCategoryHandler(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryEntity = CategoryMapper.Mapper.Map<Core.Entities.Category>(request);

            if (categoryEntity is null)
            {
                throw new ArgumentNullException("There is an issue with mapping while creating new product");
            }
            categoryEntity.CreatedAt = DateTime.UtcNow;
            categoryEntity.UpdateAt = DateTime.UtcNow;
            var newCategory = await _categoryRepository.CreateCategory(categoryEntity);
            var categoryResponse = CategoryMapper.Mapper.Map<CategoryResponse>(newCategory); //trả về cho client
            return categoryResponse;
        }
    }
}
