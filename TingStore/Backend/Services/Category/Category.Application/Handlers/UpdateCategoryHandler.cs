// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Category.Application.Commands;
using Category.Core.Repositories;
using MediatR;

namespace Category.Application.Handlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        public UpdateCategoryHandler(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var updateCategory = await _categoryRepository.UpdateCategory(new Core.Entities.Category
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
            });
            return updateCategory;
        }
    }
}
