using Library.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        //Initialzing the AppDbContext instance
        public BookRepository()
        {
            _context = new AppDbContext();
        }
        //Initializing the AppDbContext instance which it received as an argument
        public BookRepository(AppDbContext context)
        {
            _context = context;
        }
        public void CreateBook(Books books)
        {
            _context.Books.AddAsync(books);
            _context.SaveChanges();
        }

        public void DeleteBookById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Books> GetAllBooks()
        {
            return _context.Books.Include(x => x.Authores).ToList();
        }

        public Books GetBookById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(int id, Books books)
        {
            throw new NotImplementedException();
        }
    }
}
