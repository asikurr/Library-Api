using Library.Api.Entities;

namespace Library.Api.Repositories
{
    public interface IBorrowedBooksRepository
    {
        public void CreateBorrowedBook(BorrowedBooks borrowedBooks);
        public void UpdateBorrowedBook(int id, BorrowedBooks borrowedBooks);
        public BorrowedBooks GetBorrowedBookById(int id);
        public void DeleteBorrowedBookById(int id);
        public IList<BorrowedBooks> GetAllBorrowedBooks();
    }
}
