using SimpleChat.BusinessLogic.Dtos.Base;

namespace SimpleChat.BusinessLogic.Dtos
{
    public class RoomDto : BaseDto<int>
    {
        public string Name { get; set; }
    }
}
