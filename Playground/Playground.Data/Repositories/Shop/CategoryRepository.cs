using Playground.Data.Clients;
using Playground.Data.Contexts;
using Playground.Data.Models;
using Playground.Settings.Database;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Playground.Data.Repositories.Shop
{
    public class CategoryRepository : DatabaseClient, ICategoryRepository
    {
        public CategoryRepository(DatabaseSettings settings) : base(settings)
        {
        }

        public Category GetCategory(int id)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Categories.Include(c => c.BookCategories).ThenInclude(bc => bc.Book).FirstOrDefault(c => c.Id.Equals(id));
            }
        }
        public Category GetCategory(string name)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Categories.Include(c => c.BookCategories).ThenInclude(bc => bc.Book).FirstOrDefault(c => c.Name.Equals(name));
            }
        }

        public List<Category> GetCategories()
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Categories.Include(c => c.BookCategories).ThenInclude(bc => bc.Book).ToList();
            }
        }

        public bool CategoryExists(string name)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Categories.Count(c => c.Name.Equals(name)) > 0;
            }
        }

        public string Create(Category category)
        {
            string name = "NULL";

            if(category != null)
            {
                name = category.Name;
            }

            try
            {
                using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                }
                return "<p class='success'>Successfully inserted category '" + name + "'.</p>";
            }
            catch(Exception e)
            {
                return "<p class='error'>Could not insert category '" + name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br />" + e.InnerException + "</p>" : "");
            }
        }

        public string Update(Category category)
        {
            string name = "NULL";

            if(category != null)
            {
                name = category.Name;
            }

            try
            {
                using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
                {
                    db.Categories.Update(category);
                    db.SaveChanges();
                }

                return "<p class='success'>Successfully updated category '" + name + "'.</p>";
            }
            catch(Exception e)
            {
                return "<p class='error'>Could not update category '" + name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br />" + e.InnerException + "</p>" : "");
            }
        }

        public string Delete(Category category)
        {
            string name = "NULL";

            if(category != null)
            {
                name = category.Name;
            }

            try
            {
                using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
                {
                    db.BookCategory.RemoveRange(category.BookCategories);
                    db.Categories.Remove(category);
                    db.SaveChanges();
                }
                return "<p class='success'>Successfully deleted category '" + name + "'.</p>";
            }
            catch(Exception e)
            {
                return "<p class='error'>Could not delete category '" + name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br />" + e.InnerException + "</p>" : "");
            }
        }
    }
}
