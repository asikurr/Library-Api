using Library.Api.Entities;

namespace Library.Api.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;
        //Initialzing the AppDbContext instance
        public MemberRepository()
        {
            _context = new AppDbContext();
        }
        //Initializing the AppDbContext instance which it received as an argument
        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateMember(Members members)
        {
            _context.Members.AddAsync(members);
            _context.SaveChanges();
        }

        public void DeleteMemberById(int id)
        {
            var data = GetMemberById(id);
            if (data is not null)
            {
                _context.Members.Remove(data);
                _context.SaveChanges();
            }
        }

        public IList<Members> GetAllMembers()
        {
            return _context.Members.ToList();
        }

        public Members GetMemberById(int id)
        {
            return _context.Members.Where(c => c.MemberID == id).FirstOrDefault();

        }

        public void UpdateMember(int id, Members members)
        {
            var membExist = GetMemberById(id);
            if (membExist is not null)
            {
                membExist.FirstName = members.FirstName;
                membExist.LastName = members.LastName;
                membExist.Email = members.Email;
                membExist.PhoneNumber = members.PhoneNumber;
                membExist.RegistrationDate = members.RegistrationDate;
                _context.SaveChanges();
            }
        }
    }
}
