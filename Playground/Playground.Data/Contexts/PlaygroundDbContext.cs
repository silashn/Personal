using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Playground.Data.Models;
using Playground.Data.Models.Membership;
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
        public DbSet<Employee> Employees { get; set; }

        public PlaygroundDbContext(DbContextOptions<PlaygroundDbContext> options) : base(options)
        {

        }

        public PlaygroundDbContext(DatabaseSettings dbSettings)
        {
            this.dbSettings = dbSettings;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCategory>().HasKey(bc => new { bc.BookId, bc.CategoryId });
            modelBuilder.Entity<BookAuthor>().HasKey(ba => new { ba.BookId, ba.AuthorId });

            modelBuilder.Entity<BookAuthor>().HasOne(ba => ba.Book).WithMany(b => b.BookAuthors).HasForeignKey(ba => ba.BookId);
            modelBuilder.Entity<BookAuthor>().HasOne(ba => ba.Author).WithMany(c => c.BookAuthors).HasForeignKey(ba => ba.AuthorId);

            modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Book).WithMany(b => b.BookCategories).HasForeignKey(bc => bc.BookId);
            modelBuilder.Entity<BookCategory>().HasOne(bc => bc.Category).WithMany(c => c.BookCategories).HasForeignKey(bc => bc.CategoryId);
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
