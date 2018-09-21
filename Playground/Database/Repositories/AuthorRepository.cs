using Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Database.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly PlaygroundDbContext db;
        public AuthorRepository(PlaygroundDbContext _db)
        {
            db = _db;
        }

        public Author GetAuthor(int id)
        {
            return db.Authors.FirstOrDefault(a => a.Id.Equals(id));
        }

        public IEnumerable<Author> GetAuthors()
        {
            return db.Authors;
        }
    }
}
