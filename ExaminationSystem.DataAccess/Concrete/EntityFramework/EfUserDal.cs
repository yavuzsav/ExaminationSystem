using ExaminationSystem.DataAccess.Abstract;
using ExaminationSystem.DataAccess.Concrete.EntityFramework.Contexts;
using ExaminationSystem.Models.IdentityEntities;
using System.Linq;

namespace ExaminationSystem.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : IUserDal
    {
        private readonly ExaminationSystemContext _context;

        public EfUserDal(ExaminationSystemContext context)
        {
            _context = context;
        }

        public AppUser GetByUserName(string userName)
        {
            return _context.Users.SingleOrDefault(x => x.UserName == userName);
        }
    }
}
