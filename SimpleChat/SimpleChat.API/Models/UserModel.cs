using System.ComponentModel.DataAnnotations;

namespace SimpleChat.API.Models
{
    public class UserModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Nickname { get; set; }
    }
}
