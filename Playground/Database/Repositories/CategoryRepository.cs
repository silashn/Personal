using Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Database.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly PlaygroundDbContext db;

        public CategoryRepository(PlaygroundDbContext _db)
        {
            db = _db;
        }

        public Category GetCategory(int id)
        {
            return db.Categories.FirstOrDefault(c => c.Id.Equals(id));
        }

        public IEnumerable<Category> GetCategories()
        {
            return db.Categories;
        }
    }
}
