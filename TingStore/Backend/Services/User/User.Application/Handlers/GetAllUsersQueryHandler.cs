using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using User.Application.Mappers;
using User.Application.Queries;
using User.Application.Responses;
using User.Core.Repositories;


namespace User.Application.Handlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var userList = await _userRepository.GetAllUsers();
            return UserMapper.Mapper.Map<IEnumerable<UserResponse>>(userList);
        }
    }
}
