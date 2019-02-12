using Pages.Data.Scaffolding.Models;
using System.Collections.Generic;
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
        string CreateRangeVerbose(List<Theme> themes);
        string CreateRange(List<Theme> themes);
        string DeleteRangeVerbose(List<Theme> themes);
        string DeleteRange(List<Theme> themes);
    }
}
