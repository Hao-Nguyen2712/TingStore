// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Category.Application.Commands
{
    public class DeleteCategoryByIdCommand : IRequest<bool>
    {
        public string Id { get; set; }

        public DeleteCategoryByIdCommand(string id)
        {
            Id = id;
        }
    }
}
