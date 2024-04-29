using Library.Api.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Api.Repositories
{
    public class BorrowedBooksRepository : IBorrowedBooksRepository
    {
        private readonly AppDbContext _context;
        //Initialzing the AppDbContext instance
        public BorrowedBooksRepository()
        {
            _context = new AppDbContext();
        }
        //Initializing the AppDbContext instance which it received as an argument
        public BorrowedBooksRepository(AppDbContext context)
        {
            _context = context;
        }
        public void CreateBorrowedBook(BorrowedBooks borrowedBooks)
        {
            _context.BorrowedBooks.Add(borrowedBooks);
            _context.SaveChanges();
        }

        public void DeleteBorrowedBookById(int id)
        {
            var data = GetBorrowedBookById(id);
            if (data is not null)
            {
                _context.BorrowedBooks.Remove(data);
                _context.SaveChanges();
            }
        }

        public IList<BorrowedBooks> GetAllBorrowedBooks()
        {
            return _context.BorrowedBooks
                .Include(x => x.Members)
                .Include(y=> y.Books).ToList();
        }

        public BorrowedBooks GetBorrowedBookById(int id)
        {
            return _context.BorrowedBooks.Where(c => c.BorrowID == id).FirstOrDefault();
        }

        public void UpdateBorrowedBook(int id, BorrowedBooks borrowedBooks)
        {
            var bookBorExist = GetBorrowedBookById(id);
            if (bookBorExist is not null)
            {
                bookBorExist.BorrowDate = borrowedBooks.BorrowDate;
                bookBorExist.MemberID = borrowedBooks.MemberID;
                bookBorExist.BookID = borrowedBooks.BookID;
                bookBorExist.ReturnDate = borrowedBooks.ReturnDate;
                bookBorExist.Status = borrowedBooks.Status;
                _context.SaveChanges();
            }
        }
    }
}
