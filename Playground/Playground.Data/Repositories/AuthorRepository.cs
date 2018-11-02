﻿using Playground.Data.Clients;
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
                return db.Authors.FirstOrDefault(a => a.Id.Equals(id));
            }
        }

        public List<Author> GetAuthors()
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Authors.ToList();
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
                return "<p class='error'>Could not insert author '" + name + "': " + e.Message + "</p>";
            }
        }
    }
}
