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
using Category.Core.Specs;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Category.Application.Handlers
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, Pagination<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetCategoriesHandler> _logger;

        public GetCategoriesHandler(ICategoryRepository categoryRepository, ILogger<GetCategoriesHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<Pagination<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categoryList = await _categoryRepository.GetCategories(request.CategorySpecParams);
            var categoryResponseList = CategoryMapper.Mapper.Map<Pagination<CategoryResponse>>(categoryList);
            return categoryResponseList;
        }
    }
}
