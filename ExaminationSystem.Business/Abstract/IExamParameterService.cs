using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Models.Entities;

namespace ExaminationSystem.Business.Abstract
{
    public interface IExamParameterService
    {
        IResult AddOrUpdate(ExamParameter examParameter);

        IResult Delete(ExamParameter examParameter);

        IDataResult<ExamParameter> GetParameter();
    }
}