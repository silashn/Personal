using Pages.Data.Clients;
using Pages.Data.Contexts;
using Pages.Data.Models.Membership;
using Pages.Data.Repositories.Interfaces;
using Pages.Settings.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pages.Data.Repositories.Membership
{
    public class ThemeRepository : DatabaseClient, IThemeRepository
    {
        public ThemeRepository(DatabaseSettings settings) : base(settings)
        {

        }

        public Theme GetTheme(int id)
        {
            using(PagesDbContext db = new PagesDbContext(settings))
            {
                return db.Themes.FirstOrDefault(t => t.Id.Equals(id));
            }
        }

        public IQueryable<Theme> GetThemes()
        {
            using(PagesDbContext db = new PagesDbContext(settings))
            {
                return db.Themes;
            }
        }
    }
}
