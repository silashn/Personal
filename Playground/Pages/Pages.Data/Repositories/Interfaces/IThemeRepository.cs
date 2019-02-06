using Pages.Data.Scaffolding.Models;
using System.Linq;

namespace Pages.Data.Repositories.Interfaces
{
    public interface IThemeRepository
    {
        Themes GetTheme(int id);
        IQueryable<Themes> GetThemes();
        string Create(Themes theme);
        string Update(Themes theme);
        string Delete(Themes theme);
    }
}
