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
            var data = GetBookById(id);
            if (data is not null)
            {
                _context.Books.Remove(data);
                _context.SaveChanges();
            }
        }

        public IList<Books> GetAllBooks()
        {
            return _context.Books.Include(x => x.Authores).ToList();
        }

        public Books GetBookById(int id)
        {
            return _context.Books.Where(c => c.BookID == id).FirstOrDefault();
        }

        public void UpdateBook(int id, Books books)
        {
            var bookExist = GetBookById(id);
            if (bookExist is not null)
            {
                bookExist.Title = books.Title;
                bookExist.ISBN = books.ISBN;
                bookExist.AuthorID = books.AuthorID;
                bookExist.PublishedDate = books.PublishedDate;
                bookExist.AvailableCopies = books.AvailableCopies;
                bookExist.TotalCopies = books.TotalCopies;
                _context.SaveChanges();
            }
        }
    }
}
