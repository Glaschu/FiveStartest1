using Microsoft.EntityFrameworkCore;

namespace FiveStartest1.Models
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options)
            : base(options)
        {
        }

        public DbSet<GameItem> GameItems { get; set; }

    }
}