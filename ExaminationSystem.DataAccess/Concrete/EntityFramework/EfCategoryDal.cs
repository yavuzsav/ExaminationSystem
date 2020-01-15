using ExaminationSystem.DataAccess.Abstract;
using ExaminationSystem.DataAccess.Concrete.EntityFramework.Contexts;
using ExaminationSystem.Framework.Infrastructure;
using ExaminationSystem.Models.Entities;

namespace ExaminationSystem.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfRepositoryBase<Category>, ICategoryDal
    {
        public EfCategoryDal(ExaminationSystemContext context) : base(context)
        {
        }
    }
}