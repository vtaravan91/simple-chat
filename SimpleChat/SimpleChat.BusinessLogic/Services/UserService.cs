using Autofac;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleChat.BusinessLogic.Dtos;
using SimpleChat.BusinessLogic.Services.Interfaces;
using SimpleChat.DataAccess.Entities;
using SimpleChat.DataAccess.Repository;
using SimpleChat.DataAccess.Repository.Base;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity, int> _userRepository;
        private readonly IRepository<UserRoomEntity, int> _userRoomRepository;
        private readonly IMapper _mapper;
        private readonly ILifetimeScope _scope;

        public UserService(ILifetimeScope scope,
            IMapper mapper,
            IRepository<UserEntity, int> userRepository,
            IRepository<UserRoomEntity, int> userRoomRepository)
        {
            _userRepository = userRepository;
            _userRoomRepository = userRoomRepository;
            _mapper = mapper;
            _scope = scope;
        }

        public async Task<UserDto> CreateUserAsync(UserDto user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);

            var userId = await _userRepository.InsertAndGetIdAsync(userEntity);

            user.Id = userId;

            return user;
        }

        public async Task AddUserToRoom(UserRoomDto userRoom)
        {
            using (var uow = new UnitOfWork(_scope))
            {
                var userRoomEntity = _mapper.Map<UserRoomEntity>(userRoom);
                await uow.Repository<UserRoomEntity, int>().InsertAsync(userRoomEntity);
                await uow.SaveAsync();
            }
        }

        public async Task<bool> IsUserInRoom(int userId, int roomId)
        {
            bool isUserInRoom = await _userRoomRepository.Get(e => e.UserId == userId && e.RoomId == roomId).AnyAsync();

            return isUserInRoom;
        }
    }
}
