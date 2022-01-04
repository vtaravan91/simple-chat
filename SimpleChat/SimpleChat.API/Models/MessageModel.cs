using System.ComponentModel.DataAnnotations;

namespace SimpleChat.API.Models
{
    public class MessageModel
    {
        [MinLength(1)]
        [MaxLength(2000)]
        public string Message { get; set; }

        public int RoomId { get; set; }

        public int UserId { get; set; }

        public UserModel User { get; set; }
    }
}
