using Autofac;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleChat.BusinessLogic.Dtos;
using SimpleChat.BusinessLogic.Services.Interfaces;
using SimpleChat.DataAccess.Entities;
using SimpleChat.DataAccess.Repository;
using SimpleChat.DataAccess.Repository.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChat.BusinessLogic.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly ILifetimeScope _scope;
        private readonly IRepository<MessageEntity, int> _messageRepository;

        public MessageService(ILifetimeScope scope, IMapper mapper, IRepository<MessageEntity, int> messageRepository)
        {
            _scope = scope;
            _mapper = mapper;
            _messageRepository = messageRepository;
        }

        public async Task<IReadOnlyList<MessageDto>> GetMessagesAsync(MessageFilterDto filter)
        {
            var messages = await _messageRepository.Get(e => e.RoomId == filter.RoomId)
                .Include(e => e.User)
                .Skip(filter.Page * filter.Take)
                .Take(filter.Take).ToListAsync();

            return _mapper.Map<IReadOnlyList<MessageDto>>(messages);
        }

        public async Task<MessageDto> CreateMessage(MessageDto message)
        {
            var messageEntity = _mapper.Map<MessageEntity>(message);

            using (var uow = new UnitOfWork(_scope))
            {

                var messageId = await uow.Repository<MessageEntity, int>().InsertAndGetIdAsync(messageEntity);
                message.Id = messageId;

                await uow.SaveAsync();
            }

            return message;
        }
    }
}
