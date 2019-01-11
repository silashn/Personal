using Playground.Data.Models;

namespace Playground.Data.Repositories.Membership
{
    public interface IAuthorRepository
    {

        Author GetAuthor(int id);
        System.Collections.Generic.List<Author> GetAuthors();
        string Create(Author author);
        string Update(Author author);
        string Delete(Author author);
    }
}