// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Category.Application.Mappers;
using Category.Application.Queries;
using Category.Application.Responses;
using Category.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Category.Application.Handlers
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryByIdHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryById(request.Id);
            var categoryResponse = CategoryMapper.Mapper.Map<CategoryResponse>(category);
            return categoryResponse;
        }
    }
}
