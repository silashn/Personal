using Microsoft.EntityFrameworkCore;
using Playground.Data.Clients;
using Playground.Data.Contexts;
using Playground.Data.Models;
using Playground.Settings.Database;
using System;
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
                return db.Books.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author).Include(b => b.BookCategories).ThenInclude(bc => bc.Category).FirstOrDefault(b => b.Id.Equals(id));
            }
        }

        #region GET BOOKS
        public List<Book> GetBooks()
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Books.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author).Include(b => b.BookCategories).ThenInclude(bc => bc.Category).ToList();
            }
        }

        public List<Book> GetBooks(Category category)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Books.Where(b => b.BookCategories.Select(bc => bc.Category).Contains(category)).ToList();
            }
        }

        public List<Book> GetBooks(List<Category> categories)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                List<Book> books = new List<Book>();

                foreach(Category category in categories)
                {
                    books.AddRange(db.Books.Where(b => b.BookCategories.Select(bc => bc.Category).Contains(category)));
                }

                return books.Distinct().ToList();
            }
        }

        public List<Book> GetBooks(Author author)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Books.Where(b => b.BookAuthors.Select(ba => ba.Author).Contains(author)).ToList();
            }
        }

        public List<Book> GetBooks(List<Author> authors)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                List<Book> books = new List<Book>();

                foreach(Author author in authors)
                {
                    books.AddRange(db.Books.Where(b => b.BookAuthors.Select(ba => ba.Author).Contains(author)));
                }

                return books.Distinct().ToList();
            }
        }

        public List<Book> GetBooks(Author author, Category category)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Books.Where(b => b.BookAuthors.Select(ba => ba.Author).Contains(author) && b.BookCategories.Select(bc => bc.Category).Contains(category)).Distinct().ToList();
            }
        }

        public List<Book> GetBooks(List<Author> authors, List<Category> categories)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                List<Book> books = new List<Book>();

                foreach(Author author in authors)
                {
                    books.AddRange(db.Books.Where(b => b.BookAuthors.Select(ba => ba.Author).Contains(author)));
                }

                foreach(Category category in categories)
                {
                    books.AddRange(db.Books.Where(b => b.BookCategories.Select(bc => bc.Category).Contains(category)));
                }

                return books.Distinct().ToList();
            }
        }
        #endregion

        public string Insert(Book book)
        {
            string title = "NULL";
            if(book != null)
            {
                title = book.Title;
            }

            try
            {
                return "<p class='success'>Successfully inserted book '" + title + "'.";
            }
            catch(Exception e)
            {
                return "<p class='error'>Could not insert book '" + title + "': " + e.Message + "</p>";
            }
        }
    }
}
