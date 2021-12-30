using Microsoft.EntityFrameworkCore;
using SimpleChat.DataAccess.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace SimpleChat.DataAccess.Entities
{
    [Index(nameof(Nickname), IsUnique = true)]
    public sealed class UserEntity : BaseEntity<int>
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Nickname { get; set; }
    }
}
