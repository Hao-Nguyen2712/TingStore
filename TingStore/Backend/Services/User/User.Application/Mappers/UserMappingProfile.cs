// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using User.Application.Commands;
using User.Application.Responses;

namespace User.Application.Mappers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Core.Entities.User, UserResponse>().ReverseMap();
            CreateMap<Core.Entities.User, CreateUserCommand>().ReverseMap();
        }
    }
}
