using SimpleChat.BusinessLogic.Dtos;
using System.Threading.Tasks;

namespace SimpleChat.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(UserDto user);
        Task AddUserToRoom(UserRoomDto userRoom);
        Task<bool> IsUserInRoom(int userId, int roomId);
    }
}
