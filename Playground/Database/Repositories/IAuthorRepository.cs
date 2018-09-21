using Data.Models;

namespace Database.Repositories
{
    public interface IAuthorRepository
    {
        Author GetAuthor(int id);
        System.Collections.Generic.IEnumerable<Author> GetAuthors();
    }
}