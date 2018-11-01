using Playground.Data.Models;
using System.Collections.Generic;
namespace Playground.Data.Repositories
{
    public interface IBookRepository
    {
        Book GetBook(int id);
        List<Book> GetBooks();
        List<Book> GetBooks(Author author);
        List<Book> GetBooks(Author author, Category category);
        List<Book> GetBooks(Category category);
        List<Book> GetBooks(List<Author> authors);
        List<Book> GetBooks(List<Author> authors, List<Category> categories);
        List<Book> GetBooks(List<Category> categories);
    }
}