using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using SimpleChat.API.Hubs.Interfaces;
using SimpleChat.API.Models;
using SimpleChat.BusinessLogic.Dtos;
using SimpleChat.BusinessLogic.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SimpleChat.API.Hubs
{
    public class MessagesHub : Hub<IClientMessagesHub>
    {
        private readonly IMessageService _messageService;
        private readonly IRoomService _roomService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public MessagesHub(IMapper mapper, IMessageService messageService, IRoomService roomService, IUserService userService)
        {
            _messageService = messageService;
            _roomService = roomService;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task EnterRoomAsync(UserRoomModel model)
        {
            var room = await _roomService.GetByIdAsync(model.RoomId);

            if (room is { })
            {
                await _userService.AddUserToRoom(_mapper.Map<UserRoomDto>(model));
                await Groups.AddToGroupAsync(Context.ConnectionId, room.Id.ToString());
            }
            else
            {
                throw new ArgumentException($"Room #{model.RoomId} was not found");
            }
        }

        public async Task CreateMessageAsync(MessageModel model)
        {
            var messageDto = _mapper.Map<MessageDto>(model);
            var createdMessage = await _messageService.CreateMessage(messageDto);
            await this.Clients.Group(model.RoomId.ToString()).ReceiveMessageAsync(_mapper.Map<MessageModel>(createdMessage));
        }
    }
}
