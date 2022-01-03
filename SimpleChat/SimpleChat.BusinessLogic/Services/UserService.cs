using Autofac;
using AutoMapper;
using SimpleChat.BusinessLogic.Dtos;
using SimpleChat.BusinessLogic.Services.Interfaces;
using SimpleChat.DataAccess.Entities;
using SimpleChat.DataAccess.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChat.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity, int> _userRepository;
        private readonly IMapper _mapper;

        public UserService(ILifetimeScope scope,
            IMapper mapper,
            IRepository<UserEntity, int> userRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUserAsync(UserDto user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);

            var userId = await _userRepository.InsertAndGetIdAsync(userEntity);

            user.Id = userId;

            return user;
        }
    }
}
