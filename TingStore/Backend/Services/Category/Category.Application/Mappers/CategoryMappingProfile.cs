// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Category.Application.Commands;
using Category.Application.Responses;
using Category.Core.Specs;

namespace Category.Application.Mappers
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            // Mapping từ Category (Entity) sang CategoryResponse (DTO)
            CreateMap<Core.Entities.Category, CategoryResponse>().ReverseMap();

            // Mapping từ Category (Entity) sang CreateCategoryCommand (Request)
            CreateMap<Core.Entities.Category, CreateCategoryCommand>().ReverseMap();

            // Mapping từ danh sách phân trang (Pagination)
            CreateMap<Pagination<Core.Entities.Category>, Pagination<CategoryResponse>>().ReverseMap();
        }
    }
}
