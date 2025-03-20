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
    public class GetAllInactiveCategoriesHandler : IRequestHandler<GetAllInactiveCategoriesQuery, IEnumerable<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetAllInactiveCategoriesHandler> _logger;

        public GetAllInactiveCategoriesHandler(ICategoryRepository categoryRepository, ILogger<GetAllInactiveCategoriesHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryResponse>> Handle(GetAllInactiveCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categoryList = await _categoryRepository.GetAllInactiveCategories();
            var categoryResponseList = CategoryMapper.Mapper.Map<IEnumerable<CategoryResponse>>(categoryList);
            return categoryResponseList;
        }
    }
}
