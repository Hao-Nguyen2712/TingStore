// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using User.Application.Mappers;
using User.Application.Queries;
using User.Application.Responses;
using User.Core.Repositories;

namespace User.Application.Handlers
{
    public class GetAllActiveUsersQueryHandler : IRequestHandler<GetAllActiveUsersQuery, IEnumerable<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllActiveUsersQueryHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<IEnumerable<UserResponse>> Handle(GetAllActiveUsersQuery request, CancellationToken cancellationToken)
        {
            var userList = await _userRepository.GetAllActiveUsers();
            return UserMapper.Mapper.Map<IEnumerable<UserResponse>>(userList);
        }
    }
}
