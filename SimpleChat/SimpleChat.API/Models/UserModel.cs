using System.ComponentModel.DataAnnotations;

namespace SimpleChat.API.Models
{
    public class UserModel
    {
        public int? Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Nickname { get; set; }
    }
}
