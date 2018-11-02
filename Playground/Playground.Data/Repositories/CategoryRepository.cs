using Playground.Data.Clients;
using Playground.Data.Contexts;
using Playground.Data.Models;
using Playground.Settings.Database;
using System.Collections.Generic;
using System.Linq;

namespace Playground.Data.Repositories
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
                return db.Categories.FirstOrDefault(c => c.Id.Equals(id));
            }
        }
        public Category GetCategory(string name)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Categories.FirstOrDefault(c => c.Name.Equals(name));
            }
        }

        public List<Category> GetCategories()
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Categories.ToList();
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
            catch(System.Exception e)
            {
                return "<p class='error'>Could not insert category '" + name + "': " + e.Message + "</p>";
            }
        }

    }
}
