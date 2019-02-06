using Pages.Data.Repositories.Interfaces;
using Pages.Data.Scaffolding.Models;
using Pages.Data.Scaffolding.Contexts;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

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

        public string Create(Themes theme)
        {
            try
            {
                db.Themes.Add(theme);
                db.SaveChanges();
                return "<p class='success'>Successfully created theme '" + theme.Name + "'.</p>";
            }
            catch(Exception e)
            {
                return "<p class='error'>Could not create theme '" + theme.Name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br />" + e.InnerException + "</p>" : "");
            }
        }

        public string Update(Themes theme)
        {
            string name = theme.Name;

            try
            {
                Themes UpdateTheme = GetTheme(theme.Id);
                
                UpdateTheme.Name = theme.Name;
                UpdateTheme.Color = theme.Color;
                UpdateTheme.UserId = theme.UserId;
                
                db.Attach(UpdateTheme).State = EntityState.Modified;
                db.SaveChanges();
                return "<p class='success'>Successfully updated theme '" + name + "'.</p>";
            }
            catch(Exception e)
            {
                return "<p class='error'>Could not update theme '" + name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br /><p class='error inner_exception_message'>" + e.InnerException + "</p></p>" : "");
            }
        }

        public string Delete(Themes theme)
        {
            string name = theme.Name;

            try
            {
                db.Themes.Remove(theme);

                db.SaveChanges();
                return "<p class='success'>Successfully deleted theme '" + name + "'.</p>";
            }
            catch(Exception e)
            {
                return "<p class='error'>Could not delete theme '" + name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br /><p class='error inner_exception_message'>" + e.InnerException + "</p></p>" : "");
            }
        }
    }
}
