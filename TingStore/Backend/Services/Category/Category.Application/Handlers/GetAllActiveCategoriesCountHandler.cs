// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Category.Application.Queries;
using Category.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Category.Application.Handlers
{
    public class GetAllActiveCategoriesCountHandler : IRequestHandler<GetAllActiveCategoriesCountQuery, int>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetAllActiveCategoriesCountHandler> _logger;

        public GetAllActiveCategoriesCountHandler(ICategoryRepository categoryRepository, ILogger<GetAllActiveCategoriesCountHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<int> Handle(GetAllActiveCategoriesCountQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAllActiveCategoriesCount();
        }
    }
}
