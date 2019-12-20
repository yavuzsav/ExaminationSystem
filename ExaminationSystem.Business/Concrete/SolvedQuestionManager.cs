using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Business.Constants;
using ExaminationSystem.DataAccess.Abstract;
using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Framework.Utilities.Results.SuccessResults;
using ExaminationSystem.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using ExaminationSystem.Business.BusinessAspects.Autofac;

namespace ExaminationSystem.Business.Concrete
{
    [AuthenticationAspect]
    [SecuredOperation("Admin")]
    public class SolvedQuestionManager : ISolvedQuestionService
    {
        private readonly ISolvedQuestionDal _solvedQuestionDal;

        public SolvedQuestionManager(ISolvedQuestionDal solvedQuestionDal)
        {
            _solvedQuestionDal = solvedQuestionDal;
        }

        public IResult Add(SolvedQuestion solvedQuestion)
        {
            _solvedQuestionDal.Insert(solvedQuestion);
            return new SuccessResult(Messages.DeletedSuccess);
        }

        public IDataResult<List<SolvedQuestion>> GetByUserId(string userId)
        {
            return new SuccessDataResult<List<SolvedQuestion>>(_solvedQuestionDal.GetList(x => x.UserId == userId).ToList());
        }
    }
}