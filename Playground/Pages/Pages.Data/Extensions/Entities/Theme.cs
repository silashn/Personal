using Pages.Data.Repositories.Interfaces;
using Pages.Data.Repositories.Membership;
using Pages.Data.Scaffolding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pages.Data.Extensions.Entities
{
    public static class Themes
    {
        public static bool ThemeNameAlreadyExists(this Theme theme, ThemeRepository rep)
        {
            return rep.GetThemes().Any(t => t != theme && t.UserId.Equals(theme.UserId) && t.Name.Equals(theme.Name));
        }
    }
}
