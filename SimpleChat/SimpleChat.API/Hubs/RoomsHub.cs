using Microsoft.AspNetCore.SignalR;
using SimpleChat.API.Hubs.Interfaces;
using SimpleChat.API.Models;
using System.Threading.Tasks;

namespace SimpleChat.API.Hubs
{
    public class RoomsHub : Hub<IClientRoomsHub>
    {
        public async Task CreateRoomAsync(RoomModel room)
        {
            await Clients.Others.ReceiveNewRoomAsync(room);
        }
    }
}
