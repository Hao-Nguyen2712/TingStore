// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Product.Application.Responses;

namespace Product.Application.Commands
{
    public class AddImageProductCommand : IRequest<bool>
    {
        public string id { get; set; }
        public IFormFile imageFile { get; set; }
        public bool isPrimary { get; set; }
    }
}
