using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Product.Application.Responses;

namespace Product.Application.Queries
{
    public class GetAllNFProductQuery : IRequest<IEnumerable<ProductResponse>>
    {

    }
}
