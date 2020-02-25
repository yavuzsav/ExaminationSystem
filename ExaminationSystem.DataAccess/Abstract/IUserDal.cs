using ExaminationSystem.Models.IdentityEntities;

namespace ExaminationSystem.DataAccess.Abstract
{
    public interface IUserDal
    {
        AppUser GetByUserName(string userName);
    }
}
