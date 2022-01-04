using SimpleChat.API.Models;
using System.Threading.Tasks;

namespace SimpleChat.API.Hubs.Interfaces
{
    public interface IClientMessagesHub
    {
        Task ReceiveMessageAsync(MessageModel room);
    }
}
