// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace User.Application.Commands
{
    public class RestoreUserCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public RestoreUserCommand(int id) => Id = id;
    }
}
