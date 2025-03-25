// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Category.Application.Commands;
using Category.Application.Mappers;
using Category.Application.Queries;
using Category.Application.Responses;
using Category.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Category.Application.Handlers
{
    public class RestoreCategoryHandler : IRequestHandler<RestoreCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<RestoreCategoryHandler> _logger;

        public RestoreCategoryHandler(ICategoryRepository categoryRepository, ILogger<RestoreCategoryHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<bool> Handle(RestoreCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.RestoreCategory(request.Id);
        }
    }
}
