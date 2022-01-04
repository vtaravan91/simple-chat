using SimpleChat.BusinessLogic.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChat.BusinessLogic.Services.Interfaces
{
    public interface IMessageService
    {
        Task<IReadOnlyList<MessageDto>> GetMessagesAsync(MessageFilterDto filter);
        Task<MessageDto> CreateMessage(MessageDto message);
    }
}
