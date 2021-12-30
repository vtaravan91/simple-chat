using SimpleChat.DataAccess.Entities.Base;
using System.ComponentModel.DataAnnotations;


namespace SimpleChat.DataAccess.Entities
{
    public sealed class MessageEntity : BaseEntity<int>
    {
        [MinLength(1)]
        [MaxLength(2000)]        
        public string Message { get; set; }

        public int RoomId { get; set; }

        public RoomEntity Room { get; set; }

        public int UserId { get; set; }

        public UserEntity User { get; set; }
    }
}
