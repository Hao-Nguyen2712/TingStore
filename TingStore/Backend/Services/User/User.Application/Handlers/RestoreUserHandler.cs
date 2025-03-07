// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using User.Application.Commands;
using User.Core.Repositories;

namespace User.Application.Handlers
{
    public class RestoreUserHandler : IRequestHandler<RestoreUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public RestoreUserHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<bool> Handle(RestoreUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.RestoreUser(request.Id);
        }
    }
}
