using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pages.Data.Models.Membership;
using System;

namespace Pages.Data.Contexts
{
    public class PagesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Theme> Themes { get; set; }

        public PagesDbContext(DbContextOptions<PagesDbContext> options) : base(options)
        {
            
        }
    }
}
