using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Category.Application.Responses;
using MediatR;

namespace Category.Application.Commands
{
    public class CreateCategoryCommand: IRequest<CategoryResponse>
    {
        public string Name { get; set; }
    }
}
