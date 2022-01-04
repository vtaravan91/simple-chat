using SimpleChat.BusinessLogic.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleChat.BusinessLogic.Services.Interfaces
{
    public interface IRoomService
    {
        Task<RoomDto> CreateRoomAsync(RoomDto room);
        Task<IReadOnlyList<RoomDto>> GetAllAsync(RoomsFilterDto filter);
        Task<RoomDto> GetByIdAsync(int roomId);
    }
}
