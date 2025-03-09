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

namespace Category.Application.Handlers
{
    public class DeleteCategoryByIdHandler : IRequestHandler<DeleteCategoryByIdQuery, bool>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryByIdHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(DeleteCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.DeleteCategory(request.Id);
        }
    }
}
