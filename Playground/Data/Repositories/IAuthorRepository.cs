using Data.Models;

namespace Data.Repositories
{
    public interface IAuthorRepository
    {
        Author GetAuthor(int id);
        System.Collections.Generic.IEnumerable<Author> GetAuthors();
    }
}