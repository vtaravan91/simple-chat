using SimpleChat.BusinessLogic.Dtos.Base;

namespace SimpleChat.BusinessLogic.Dtos
{
    public class MessageDto: BaseDto<int>
    {
        public string Message { get; set; }

        public int RoomId { get; set; }

        public RoomDto Room { get; set; }

        public int UserId { get; set; }

        public UserDto User { get; set; }
    }
}
