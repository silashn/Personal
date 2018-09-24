using Settings.Database;
using Microsoft.EntityFrameworkCore;

namespace Database.Contexts
{
    public class PlaygroundDbContext : DbContext
    {
        private readonly DatabaseSettings dbSettings;

        public PlaygroundDbContext(DbContextOptions<PlaygroundDbContext> options) : base(options)
        {

        }

        public PlaygroundDbContext(DatabaseSettings dbSettings)
        {
            this.dbSettings = dbSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(dbSettings.ConnectionString);
        }
    }

}
