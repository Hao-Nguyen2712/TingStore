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
    public class GetAllInactiveUsersQueryHandler : IRequestHandler<GetAllInactiveUsersQuery, IEnumerable<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllInactiveUsersQueryHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<IEnumerable<UserResponse>> Handle(GetAllInactiveUsersQuery request, CancellationToken cancellationToken)
        {
            var userList = await _userRepository.GetAllInactiveUsers();
            return UserMapper.Mapper.Map<IEnumerable<UserResponse>>(userList);
        }
    }
}
