using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Models.Entities;
using System.Collections.Generic;

namespace ExaminationSystem.Business.Abstract
{
    public interface ISolvedQuestionService
    {
        IResult Add(SolvedQuestion solvedQuestion);

        IDataResult<List<SolvedQuestion>> GetByUserId(string userId);
    }
}