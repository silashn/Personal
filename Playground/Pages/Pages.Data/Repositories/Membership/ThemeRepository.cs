﻿using Microsoft.EntityFrameworkCore;
using Pages.Data.Repositories.Interfaces;
using Pages.Data.Scaffolding.Contexts;
using Pages.Data.Scaffolding.Models;
using Pages.Data.Extensions.Entities;
using System;
using System.Collections.Generic;
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

        public Theme GetTheme(int id)
        {
            return db.Themes.FirstOrDefault(t => t.Id.Equals(id));
        }

        public IQueryable<Theme> GetThemes()
        {
            return db.Themes;
        }

        public string Create(Theme theme)
        {
            try
            {
                if(theme.ThemeNameAlreadyExists(this))
                {
                    throw new InvalidOperationException("User already has a theme by that name.");
                }

                db.Themes.Add(theme);
                db.SaveChanges();
                return "<p class='success'>Successfully created theme '" + theme.Name + "'.</p>";
            }
            catch(Exception e)
            {
                return "<p class='error'>Could not create theme '" + theme.Name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br />" + e.InnerException + "</p>" : "");
            }
        }

        public string Update(Theme theme)
        {
            string name = theme.Name;

            try
            {
                if(theme.ThemeNameAlreadyExists(this))
                {
                    throw new InvalidOperationException("User already has a theme by that name.");
                }

                db.Attach(theme).State = EntityState.Modified;
                db.SaveChanges();
                return "<p class='success'>Successfully updated theme '" + name + "'.</p>";
            }
            catch(Exception e)
            {
                return "<p class='error'>Could not update theme '" + name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br /><p class='error inner_exception_message'>" + e.InnerException + "</p></p>" : "");
            }
        }

        public string Delete(Theme theme)
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

        public string CreateRangeVerbose(List<Theme> themes)
        {
            string message = "";
            string errors = "";
            db.ChangeTracker.AutoDetectChangesEnabled = false;
            foreach(Theme theme in themes)
            {
                string name = theme.Name;

                try
                {
                    if(theme.ThemeNameAlreadyExists(this))
                    {
                        throw new InvalidOperationException("User already has a theme by that name.");
                    }

                    db.Themes.Add(theme);
                    message += "<p class='success'>Successfully created theme '" + name + "'.</p>";
                }
                catch(Exception e)
                {
                    if(errors == string.Empty)
                    {
                        errors += "<p class='error'>ERRORS:<br />Could not create theme '" + name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br /><p class='error inner_exception_message'>" + e.InnerException + "</p></p>" : "");
                    }
                    else
                    {
                        errors += "<p class='error'>Could not create theme '" + name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br /><p class='error inner_exception_message'>" + e.InnerException + "</p></p>" : "");
                    }
                }
            }

            if(errors == string.Empty)
            {
                db.ChangeTracker.DetectChanges();
                db.SaveChanges();

                return message;
            }

            return errors;
        }

        public string CreateRange(List<Theme> themes)
        {
            int count = themes.Count;
            try
            {
                db.Themes.AddRange(themes);
                db.SaveChanges();
                return $"<p class='success'>Successfully created {count} themes.</p>";
            }
            catch(Exception e)
            {
                return $"<p class='error'>Could not delete {count} themes: " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br /><p class='error inner_exception_message'>" + e.InnerException + "</p></p>" : "");
            }
        }

        public string DeleteRangeVerbose(List<Theme> themes)
        {
            string message = "";
            string errors = "";
            
            db.ChangeTracker.AutoDetectChangesEnabled = false;
            foreach(Theme theme in themes)
            {
                string name = theme.Name;

                try
                {
                    db.Themes.Remove(theme);                    
                    message += "<p class='success'>Successfully deleted theme '" + name + "'.</p>";
                }
                catch(Exception e)
                {
                    if(errors == string.Empty)
                    {
                        errors += "<p class='error'>ERRORS:<br />Could not delete theme '" + name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br /><p class='error inner_exception_message'>" + e.InnerException + "</p></p>" : "");
                    }
                    else
                    {
                        errors += "<p class='error'>Could not delete theme '" + name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br /><p class='error inner_exception_message'>" + e.InnerException + "</p></p>" : "");
                    }
                }

                if(themes.IndexOf(theme) < themes.Count && themes.IndexOf(theme) % 15000 == 0)
                {
                    db.ChangeTracker.DetectChanges();
                    db.SaveChanges();
                    db.ChangeTracker.AutoDetectChangesEnabled = false;
                }
            }

            if(errors == string.Empty)
            {
                db.ChangeTracker.DetectChanges();
                db.SaveChanges();

                return message;
            }

            return errors;
        }

        public string DeleteRange(List<Theme> themes)
        {
            int count = themes.Count;
            try
            {
                db.Themes.RemoveRange(themes);
                db.SaveChanges();
                return $"<p class='success'>Successfully deleted {count} themes.</p>";
            }
            catch(Exception e)
            {
                return $"<p class='error'>Could not delete {count} themes: " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br /><p class='error inner_exception_message'>" + e.InnerException + "</p></p>" : "");
            }
        }
    }
}