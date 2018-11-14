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
    public class AuthorRepository : DatabaseClient, IAuthorRepository
    {
        public AuthorRepository(DatabaseSettings settings) : base(settings)
        {
        }

        public Author GetAuthor(int id)
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Authors.Include(a => a.BookAuthors).ThenInclude(ba => ba.Book).FirstOrDefault(a => a.Id.Equals(id));
            }
        }

        public List<Author> GetAuthors()
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Authors.Include(a => a.BookAuthors).ThenInclude(ba => ba.Book).ToList();
            }
        }

        public string Create(Author author)
        {
            string name = "NULL";
            if(author != null)
            {
                name = author.Name;
            }

            try
            {
                using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
                {
                    db.Authors.Add(author);
                    db.SaveChanges();
                }

                return "<p class='success'>Successfully inserted author '" + name + "'.</p>";
            }
            catch(Exception e)
            {
                return "<p class='error'>Could not insert author '" + name + "': " + e.Message + "</p>" + "</p>" + (e.InnerException != null ? "<p style='color:#FA5;'><b><i>Inner exception:</i></b><br />" + e.InnerException + "</p>" : "");
            }
        }

        public string Update(Author author)
        {
            string name = "NULL";
            if(author != null)
            {
                name = author.Name;
            }

            try
            {
                using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
                {
                    Author updateAuthor = db.Authors.FirstOrDefault(a => a.Id.Equals(author.Id));
                    updateAuthor.Name = author.Name;
                    db.Authors.Update(updateAuthor);
                    db.SaveChanges();
                }

                return "<p class='success'>Successfully updated author '" + name + "'.</p>";
            }
            catch(Exception e)
            {
                return "<p class='error'>Could not update author '" + name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p style='color:#FA5;'><b><i>Inner exception:</i></b><br />" + e.InnerException + "</p>" : "");
            }
        }
    }
}
