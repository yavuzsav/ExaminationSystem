using ExaminationSystem.DataAccess.Abstract;
using ExaminationSystem.DataAccess.Concrete.EntityFramework.Contexts;
using ExaminationSystem.Framework.Infrastructure;
using ExaminationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.DataAccess.Concrete.EntityFramework
{
    public class EfExamParameterDal : EfRepositoryBase<ExamParameter, ExaminationSystemContext>, IExamParameterDal
    {
        public EfExamParameterDal(ExaminationSystemContext context) : base(context)
        {
        }
    }
}