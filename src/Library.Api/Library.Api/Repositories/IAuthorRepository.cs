using Library.Api.Entities;

namespace Library.Api.Repositories
{
    public interface IAuthorRepository
    {
       public void CreateAuthor(Authors authors);
       public void UpdateAuthor(int id,Authors authors);
       public Authors GetAuthorById(int id);
        public void DeleteAuthorById(int id);
        public IList<Authors> GetAllAuthors();
    }
}
