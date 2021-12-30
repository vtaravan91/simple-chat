using SimpleChat.DataAccess.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace SimpleChat.DataAccess.Entities
{
    public sealed class RoomEntity : BaseEntity<int>
    {
        [Required]
        [MinLength(2)]
        [MaxLength(256)]       
        public string Name { get; set; }
    }
}
