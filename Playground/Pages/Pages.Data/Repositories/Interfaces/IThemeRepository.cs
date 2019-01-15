using Pages.Data.Models.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pages.Data.Repositories.Interfaces
{
    public interface IThemeRepository
    {
        Theme GetTheme(int id);
        IQueryable<Theme> GetThemes();
    }
}
