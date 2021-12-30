using Microsoft.EntityFrameworkCore;
using SimpleChat.DataAccess.Entities;

namespace SimpleChat.DataAccess.DBContext
{
    internal sealed class SimpleChatContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoomEntity> Rooms { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }
        public SimpleChatContext(DbContextOptions<SimpleChatContext> options)
            : base(options)
        {

        }

    }
}
