using Library.Api.Entities;

namespace Library.Api.Repositories
{
    public interface IMemberRepository
    {
       public void CreateMember(Members members);
       public void UpdateMember(int id,Members members);
       public Members GetMemberById(int id);
        public void DeleteMemberById(int id);
        public IList<Members> GetAllMembers();
    }
}
