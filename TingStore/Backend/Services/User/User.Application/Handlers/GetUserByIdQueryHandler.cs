using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using User.Application.Mappers;
using User.Application.Queries;
using User.Application.Responses;
using User.Core.Repositories;

namespace User.Application.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id);
            if (user == null)
               throw new KeyNotFoundException("User not found!");

            return UserMapper.Mapper.Map<UserResponse>(user);
        }
    }
}
