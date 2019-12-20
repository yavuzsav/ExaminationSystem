using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Business.Constants;
using ExaminationSystem.DataAccess.Abstract;
using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Framework.Utilities.Results.SuccessResults;
using ExaminationSystem.Models.Entities;
using System.Linq;
using ExaminationSystem.Business.BusinessAspects.Autofac;

namespace ExaminationSystem.Business.Concrete
{
    [AuthenticationAspect]
    [SecuredOperation("Admin")]
    public class ExamParameterManager : IExamParameterService
    {
        private readonly IExamParameterDal _examParameterDal;

        public ExamParameterManager(IExamParameterDal examParameterDal)
        {
            _examParameterDal = examParameterDal;
        }

        public IResult AddOrUpdate(ExamParameter examParameter)
        {
            var list = _examParameterDal.GetList();

            if (list.Any())
            {
                list[0].LengthOfExam = examParameter.LengthOfExam;
                list[0].NumberOfQuestions = examParameter.NumberOfQuestions;
                _examParameterDal.Update(list[0]);
                return new SuccessResult(Messages.UpdatedSuccess);
            }

            examParameter.Id = "1";
            _examParameterDal.Insert(examParameter);
            return new SuccessResult(Messages.AddedSuccess);
        }

        public IResult Delete(ExamParameter examParameter)
        {
            _examParameterDal.Delete(examParameter);
            return new SuccessResult(Messages.DeletedSuccess);
        }

        public IDataResult<ExamParameter> GetParameter()
        {
            var item = _examParameterDal.GetList().LastOrDefault();
            return new SuccessDataResult<ExamParameter>(item);
        }
    }
}