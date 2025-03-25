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

namespace Category.Application.Handlers
{
    public class GetCategoryByNameHandler : IRequestHandler<GetCategoryByNameQuery, IEnumerable<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryByNameHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<CategoryResponse>> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var categoryByName = await _categoryRepository.GetCategoryByName(request.Name);
            var categoryResponse = CategoryMapper.Mapper.Map<IEnumerable<CategoryResponse>>(categoryByName);
            return categoryResponse;
        }
    }
}
