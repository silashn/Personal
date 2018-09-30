using Playground.Data.Models;

namespace Playground.Data.Repositories
{
    public interface ICategoryRepository
    {
        Category GetCategory(int id);
        Category GetCategory(string name);
        bool CategoryExists(string name);
        System.Collections.Generic.List<Category> GetCategories();
        void Create(Category category);
    }
}