
using Library.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace Library.Api.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;
        //Initialzing the AppDbContext instance
        public AuthorRepository()
        {
            _context = new AppDbContext();
        }
        //Initializing the AppDbContext instance which it received as an argument
        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }
        public void CreateAuthor(Authors authors)
        {
             _context.Authors.AddAsync(authors);
             _context.SaveChanges();
        }

        public void DeleteAuthorById(int id)
        {
          
                var data = GetAuthorById(id);
                if (data is not null)
                {
                    _context.Authors.Remove(data);
                    _context.SaveChanges();
                }
           
        }

        public IList<Authors> GetAllAuthors()
        {
           return _context.Authors.ToList();
        }

        public  Authors GetAuthorById(int id)
        {
          return _context.Authors.Where( c => c.AuthorID ==  id).FirstOrDefault();
        }

        public void UpdateAuthor(int id, Authors authors)
        {

                var authExist = GetAuthorById(id);
                if (authExist is not null)
                {
                    authExist.AuthorName = authors.AuthorName;
                    authExist.AuthorBio = authors.AuthorBio;
                    _context.SaveChanges();
                }
        }
    }
}
