using Playground.Data.Models;

namespace Playground.Data.Repositories
{
    public interface IBookRepository
    {
        Book GetBook(int id);
        System.Collections.Generic.List<Book> GetBooks();
        System.Collections.Generic.List<Book> GetBooks(Author author);
        System.Collections.Generic.List<Book> GetBooks(Author author, Category category);
        System.Collections.Generic.List<Book> GetBooks(Category category);
        System.Collections.Generic.List<Book> GetBooks(System.Collections.Generic.List<Author> authors);
        System.Collections.Generic.List<Book> GetBooks(System.Collections.Generic.List<Author> authors, System.Collections.Generic.List<Category> categories);
        System.Collections.Generic.List<Book> GetBooks(System.Collections.Generic.List<Category> categories);
    }
}