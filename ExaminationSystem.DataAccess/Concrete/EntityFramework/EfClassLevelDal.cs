using ExaminationSystem.DataAccess.Abstract;
using ExaminationSystem.DataAccess.Concrete.EntityFramework.Contexts;
using ExaminationSystem.Framework.Infrastructure;
using ExaminationSystem.Models.Entities;

namespace ExaminationSystem.DataAccess.Concrete.EntityFramework
{
    public class EfClassLevelDal : EfRepositoryBase<ClassLevel>, IClassLevelDal
    {
        public EfClassLevelDal(ExaminationSystemContext context) : base(context)
        {
        }
    }
}