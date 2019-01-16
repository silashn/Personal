using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pages.Data.Scaffolding.Models;

namespace Pages.Data.Scaffolding.Contexts
{
    public partial class PagesDbContext : DbContext
    {
        public PagesDbContext()
        {
        }

        public PagesDbContext(DbContextOptions<PagesDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Themes> Themes { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Initial Catalog=PlaygroundPages;AttachDBFilename=C:\\Users\\Silas\\Documents\\PersonalRepository\\Playground\\Pages\\Pages.Web\\App_Data\\PlaygroundPages.mdf; Integrated Security=true; MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Themes>(entity =>
            {
                entity.HasIndex(e => e.UserId);
            });
        }
    }
}
