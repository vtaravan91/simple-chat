using System.ComponentModel.DataAnnotations;

namespace SimpleChat.API.Models
{
    public class RoomModel
    {
        public int? Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(256)]
        public string Name { get; set; }
        public bool? UserInRoom { get; set; }
    }
}
