using System.Collections.Generic;
using Data.Models;

namespace Database.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
    }
}