using Pages.Data.Repositories.Interfaces;
using Pages.Data.Scaffolding.Models;
using Pages.Data.Scaffolding.Contexts;
using System.Linq;

namespace Pages.Data.Repositories.Membership
{
    public class ThemeRepository : IThemeRepository
    {
        private readonly PagesDbContext db;

        public ThemeRepository(PagesDbContext db)
        {
            this.db = db;
        }

        public Themes GetTheme(int id)
        {
            return db.Themes.FirstOrDefault(t => t.Id.Equals(id));
        }

        public IQueryable<Themes> GetThemes()
        {
            return db.Themes;
        }
    }
}
