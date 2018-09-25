using Playground.Data.Clients;
using Playground.Data.Contexts;
using Playground.Data.Models;
using Playground.Settings.Database;
using System.Collections.Generic;
using System.Linq;

namespace Playground.Data.Repositories
{
    public class BookRepository : DatabaseClient, IBookRepository
    {
        public BookRepository(DatabaseSettings settings) : base(settings)
        {
        }

        public Book GetBook(int id)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Books.FirstOrDefault(b => b.Id.Equals(id));
            }
        }

        public List<Book> GetBooks()
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Books.ToList();
            }
        }

        public IEnumerable<Book> GetBooks(Category category)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Books.Where(b => b.Categories.Contains(category));
            }
        }

        public IEnumerable<Book> GetBooks(List<Category> categories)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                List<Book> books = new List<Book>();

                foreach(Category category in categories)
                {
                    books.AddRange(db.Books.Where(b => b.Categories.Contains(category)));
                }

                return books.Distinct();
            }
        }

        public IEnumerable<Book> GetBooks(Author author)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Books.Where(b => b.Authors.Contains(author));
            }
        }

        public IEnumerable<Book> GetBooks(List<Author> authors)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                List<Book> books = new List<Book>();

                foreach(Author author in authors)
                {
                    books.AddRange(db.Books.Where(b => b.Authors.Contains(author)));
                }

                return books.Distinct();
            }
        }

        public IEnumerable<Book> GetBooks(Author author, Category category)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Books.Where(b => b.Authors.Contains(author) && b.Categories.Contains(category)).Distinct();
            }
        }

        public IEnumerable<Book> GetBooks(List<Author> authors, List<Category> categories)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                List<Book> books = new List<Book>();

                foreach(Author author in authors)
                {
                    books.AddRange(db.Books.Where(b => b.Authors.Contains(author)));
                }

                foreach(Category category in categories)
                {
                    books.AddRange(db.Books.Where(b => b.Categories.Contains(category)));
                }

                return books.Distinct();
            }
        }
    }
}
