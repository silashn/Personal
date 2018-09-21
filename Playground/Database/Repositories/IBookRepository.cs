using System.Collections.Generic;
using Data.Models;

namespace Database.Repositories
{
    public interface IBookRepository
    {
        Book GetBook(int id);
        IEnumerable<Book> GetBooks();
        IEnumerable<Book> GetBooks(Author author);
        IEnumerable<Book> GetBooks(Author author, Category category);
        IEnumerable<Book> GetBooks(Category category);
        IEnumerable<Book> GetBooks(List<Author> authors);
        IEnumerable<Book> GetBooks(List<Author> authors, List<Category> categories);
        IEnumerable<Book> GetBooks(List<Category> categories);
    }
}