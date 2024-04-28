using Library.Api.Entities;

namespace Library.Api.Repositories
{
    public interface IBookRepository
    {
        public void CreateBook(Books books);
        public void UpdateBook(int id, Books books);
        public Books GetBookById(int id);
        public void DeleteBookById(int id);
        public IList<Books> GetAllBooks();
    }
}
