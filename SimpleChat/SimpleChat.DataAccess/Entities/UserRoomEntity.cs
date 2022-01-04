using SimpleChat.DataAccess.Entities.Base;

namespace SimpleChat.DataAccess.Entities
{
    public class UserRoomEntity : BaseEntity<int>
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }

        public UserEntity User { get; set; }
        public RoomEntity Room { get; set; }
    }
}
