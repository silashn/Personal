using Playground.Data.Models;

namespace Playground.Data.Repositories.Shop
{
    public interface ICategoryRepository
    {
        Category GetCategory(int id);
        Category GetCategory(string name);
        bool CategoryExists(string name);
        System.Collections.Generic.List<Category> GetCategories();
        string Create(Category category);
        string Update(Category category);
        string Delete(Category category);
    }
}