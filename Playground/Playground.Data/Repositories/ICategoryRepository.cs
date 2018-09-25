using Playground.Data.Models;

namespace Playground.Data.Repositories
{
    public interface ICategoryRepository
    {
        System.Collections.Generic.IEnumerable<Category> GetCategories();
        Category GetCategory(int id);
    }
}