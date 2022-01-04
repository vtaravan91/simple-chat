using Autofac;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleChat.BusinessLogic.Dtos;
using SimpleChat.BusinessLogic.Services.Interfaces;
using SimpleChat.DataAccess.Entities;
using SimpleChat.DataAccess.Repository;
using SimpleChat.DataAccess.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimpleChat.BusinessLogic.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRepository<RoomEntity, int> _roomRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILifetimeScope _scope;

        public RoomService(ILifetimeScope scope, IMapper mapper, IRepository<RoomEntity, int> roomRepository, IUserService userService)
        {
            _roomRepository = roomRepository;
            _userService = userService;
            _mapper = mapper;
            _scope = scope;
        }

        public async Task<RoomDto> CreateRoomAsync(RoomDto room)
        {
            var roomEntity = _mapper.Map<RoomEntity>(room);

            using (var uow = new UnitOfWork(_scope))
            {
                var dbRoom = await uow.Repository<RoomEntity, int>().Get(e => e.Name.Equals(room.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefaultAsync();

                if (dbRoom is { })
                {
                    throw new ArgumentException("Room name should be unique", nameof(room.Name));
                }

                var roomId = await uow.Repository<RoomEntity, int>().InsertAndGetIdAsync(roomEntity);
                room.Id = roomId;

                await uow.SaveAsync();
            }

            return room;
        }

        public async Task<IReadOnlyList<RoomDto>> GetAllAsync(RoomsFilterDto filter)
        {
            Expression<Func<RoomEntity, bool>> expression = e => true;

            if (filter?.RoomId != null)
            {
                Expression<Func<RoomEntity, bool>> roomIdExpression = e => e.Id == filter.RoomId;
                expression = Expression.Lambda<Func<RoomEntity, bool>>(Expression.AndAlso(expression.Body,
                    Expression.Invoke(roomIdExpression, expression.Parameters)),
                    expression.Parameters);
            }

            var rooms = await _roomRepository.Get(expression).ToListAsync();

            var roomDtos = _mapper.Map<IReadOnlyList<RoomDto>>(rooms);

            if (filter?.CheckUserInRoom == true)
            {
                foreach (var room in roomDtos)
                {
                    room.UserInRoom = await _userService.IsUserInRoom(filter.UserId.Value, room.Id);
                }
            }

            return roomDtos;
        }

        public async Task<RoomDto> GetByIdAsync(int roomId)
        {
            var room = await _roomRepository.GetByIdAsync(roomId);

            return _mapper.Map<RoomDto>(room);
        }
    }
}
