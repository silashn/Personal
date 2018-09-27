using Playground.Data.Models;

namespace Playground.Data.Repositories
{
    public interface IAuthorRepository
    {
        Author GetAuthor(int id);
        System.Collections.Generic.List<Author> GetAuthors();
        void Create(Author author);
    }
}