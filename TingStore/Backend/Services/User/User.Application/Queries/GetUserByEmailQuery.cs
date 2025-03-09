// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using User.Application.Responses;

namespace User.Application.Queries
{
    public class GetUserByEmailQuery : IRequest<UserResponse>
    {
        public string Email { get; set; }

        public GetUserByEmailQuery(string email) => Email = email;
    }
}
