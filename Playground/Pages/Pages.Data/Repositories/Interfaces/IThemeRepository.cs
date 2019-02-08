using Pages.Data.Scaffolding.Models;
using System.Linq;

namespace Pages.Data.Repositories.Interfaces
{
    public interface IThemeRepository
    {
        Theme GetTheme(int id);
        IQueryable<Theme> GetThemes();
        string Create(Theme theme);
        string Update(Theme theme);
        string Delete(Theme theme);
    }
}
