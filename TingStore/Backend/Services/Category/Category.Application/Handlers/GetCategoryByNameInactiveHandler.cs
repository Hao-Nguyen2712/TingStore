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
    public class GetCategoryByNameInactiveHandler : IRequestHandler<GetCategoryByNameInactiveQuery, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryByNameInactiveHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryResponse> Handle(GetCategoryByNameInactiveQuery request, CancellationToken cancellationToken)
        {
            var categoryByName = await _categoryRepository.GetCategoryByNameInactive(request.Name);

            if (categoryByName == null)
            {
                return null; 
            }

            return CategoryMapper.Mapper.Map<CategoryResponse>(categoryByName);
        }

    }
}
