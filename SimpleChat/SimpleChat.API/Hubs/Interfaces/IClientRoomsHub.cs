using SimpleChat.API.Models;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleChat.API.Hubs.Interfaces
{
    public interface IClientRoomsHub
    {
        Task ReceiveNewRoomAsync(RoomModel room);
    }
}
