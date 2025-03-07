using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using User.Application.Responses;

namespace User.Application.Queries
{
    public class GetAllInactiveUsersQuery : IRequest<IEnumerable<UserResponse>>
    {
    }
}
