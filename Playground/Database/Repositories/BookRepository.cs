using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;

namespace Database.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly PlaygroundDbContext db;

        public BookRepository(PlaygroundDbContext _db)
        {
            db = _db;
        }

        public Book GetBook(int id)
        {
            return db.Books.FirstOrDefault(b => b.Id.Equals(id));
        }

        public IEnumerable<Book> GetBooks()
        {
            return db.Books;
        }

        public IEnumerable<Book> GetBooks(Category category)
        {
            return db.Books.Where(b => b.Categories.Contains(category));
        }

        public IEnumerable<Book> GetBooks(List<Category> categories)
        {
            List<Book> books = new List<Book>();

            foreach(Category category in categories)
            {
                books.AddRange(db.Books.Where(b => b.Categories.Contains(category)));
            }

            return books.Distinct();
        }

        public IEnumerable<Book> GetBooks(Author author)
        {
            return db.Books.Where(b => b.Authors.Contains(author));
        }

        public IEnumerable<Book> GetBooks(List<Author> authors)
        {
            List<Book> books = new List<Book>();

            foreach(Author author in authors)
            {
                books.AddRange(db.Books.Where(b => b.Authors.Contains(author)));
            }

            return books.Distinct();
        }

        public IEnumerable<Book> GetBooks(Author author, Category category)
        {
            return db.Books.Where(b => b.Authors.Contains(author) && b.Categories.Contains(category)).Distinct();
        }

        public IEnumerable<Book> GetBooks(List<Author> authors, List<Category> categories)
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
