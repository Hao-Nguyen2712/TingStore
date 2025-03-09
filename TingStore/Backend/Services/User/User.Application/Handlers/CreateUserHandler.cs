using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using User.Application.Commands;
using User.Application.Mappers;
using User.Application.Responses;
using User.Core.Repositories;

namespace User.Application.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserResponse>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserByEmail(request.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists.");
            }

            // Ánh xạ request thành entity
            var userEntity = UserMapper.Mapper.Map<Core.Entities.User>(request) ??
                throw new ApplicationException("Mapping failed while creating a new user.");

            // Lưa vào db
            var newUser = await _userRepository.CreateUser(userEntity);

            // Anh xạ entity sang response
            var productResponse = UserMapper.Mapper.Map<UserResponse>(newUser);

            return productResponse;

        }
    }
}
