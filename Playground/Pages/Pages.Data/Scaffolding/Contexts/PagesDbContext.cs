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

        public virtual DbSet<Theme> Themes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Theme>(entity =>
            {
                entity.HasIndex(e => e.UserId);
            });
        }
    }
}
