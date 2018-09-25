using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Playground.Data.Models;
using Playground.Settings.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Data.Contexts
{
    public class PlaygroundDbContext : DbContext
    {
        private readonly DatabaseSettings dbSettings;

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        public PlaygroundDbContext(DbContextOptions<PlaygroundDbContext> options) : base(options)
        {

        }

        public PlaygroundDbContext(DatabaseSettings dbSettings)
        {
            this.dbSettings = dbSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(dbSettings.ConnectionString.Contains("%ROOTPATH%"))
            {
                optionsBuilder.UseSqlServer(dbSettings.ConnectionString.Replace("%ROOTPATH%", Directory.GetParent(AppContext.BaseDirectory + "..\\..\\..\\").FullName));
            }
            else
            {
                optionsBuilder.UseSqlServer(dbSettings.ConnectionString);
            }
        }
    }
}
