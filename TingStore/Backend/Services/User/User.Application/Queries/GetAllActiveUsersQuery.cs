using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using User.Application.Responses;

namespace User.Application.Queries
{
    public class GetAllActiveUsersQuery : IRequest<IEnumerable<UserResponse>>
    {
    }
}
