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

        public IEnumerable<Category> GetCategories()
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Categories;
            }
        }

    }
}
