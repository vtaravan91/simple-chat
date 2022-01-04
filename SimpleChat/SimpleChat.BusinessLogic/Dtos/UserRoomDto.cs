namespace SimpleChat.BusinessLogic.Dtos
{
    public class UserRoomDto
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public UserDto User { get; set; }
        public RoomDto Room { get; set; }
    }
}
