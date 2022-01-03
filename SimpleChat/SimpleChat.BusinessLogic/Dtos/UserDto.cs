using SimpleChat.BusinessLogic.Dtos.Base;

namespace SimpleChat.BusinessLogic.Dtos
{
    public class UserDto : BaseDto<int>
    {
        public string Nickname { get; set; }
    }
}
