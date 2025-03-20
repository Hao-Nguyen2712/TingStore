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
    public class GetAllActiveCategoriesHandler : IRequestHandler<GetAllActiveCategoriesQuery, IEnumerable<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetAllActiveCategoriesHandler> _logger;

        public GetAllActiveCategoriesHandler(ICategoryRepository categoryRepository, ILogger<GetAllActiveCategoriesHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryResponse>> Handle(GetAllActiveCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categoryList = await _categoryRepository.GetAllActiveCategories();
            var categoryResponseList = CategoryMapper.Mapper.Map<IEnumerable<CategoryResponse>>(categoryList);
            return categoryResponseList;
        }
    }
}
