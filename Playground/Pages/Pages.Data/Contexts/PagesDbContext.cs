using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pages.Data.Models.Membership;
using Pages.Settings.Database;

namespace Pages.Data.Contexts
{
    public class PagesDbContext : DbContext
    {
        private readonly DatabaseSettings settings;

        public DbSet<User> Users { get; set; }
        public DbSet<Theme> Themes { get; set; }

        public PagesDbContext(DbContextOptions<PagesDbContext> options) : base(options)
        {

        }

        public PagesDbContext(DatabaseSettings settings)
        {
            this.settings = settings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if(settings.ConnectionString.Contains("%ROOTPATH%"))
            {
                builder.UseSqlServer(settings.ConnectionString.Replace("%ROOTPATH%", Directory.GetParent(AppContext.BaseDirectory + "..\\..\\..\\").FullName));
            }
            else
            {
                builder.UseSqlServer(settings.ConnectionString);
            }
        }
    }
}
