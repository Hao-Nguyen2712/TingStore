// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using User.Application.Commands;
using User.Application.Responses;
using User.Core.Repositories;

namespace User.Application.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found!");
            }

            //var updatedUser = _mapper.Map(request, user);
            if(!string.IsNullOrEmpty(request.Email) &&  request.Email != user.Email)
            {
                var existingUser = await _userRepository.GetUserByEmail(request.Email);
                if(existingUser != null && existingUser.Id != user.Id)
                {
                    throw new Exception("Email already exists");
                }
            }

            user.Email = request.Email ?? user.Email;
            user.Password = request.Password ?? user.Password;
            user.FullName = request.FullName ?? user.FullName;
            user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            user.Address = request.Address ?? user.Address;
            user.isActive = request.IsActive;
            user.UpdatedAt = DateTime.UtcNow;
            user.CreatedAt = user.CreatedAt;

            var result = await _userRepository.UpdateUser(user);
            // Trả về response (DTO) cho API
            return _mapper.Map<UserResponse>(user);
        }
    }
}
