using Playground.Data.Models;

namespace Playground.Data.Repositories
{
    public interface ICategoryRepository
    {
        System.Collections.Generic.List<Category> GetCategories();
        Category GetCategory(int id);
        void Create(Category category);
    }
}