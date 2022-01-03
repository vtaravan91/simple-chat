using SimpleChat.BusinessLogic.Dtos;
using System.Threading.Tasks;

namespace SimpleChat.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(UserDto user);
    }
}
