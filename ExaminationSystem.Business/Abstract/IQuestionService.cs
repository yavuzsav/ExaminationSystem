using ExaminationSystem.Business.Helpers;
using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Models.Dtos.User;
using ExaminationSystem.Models.Entities;
using System.Collections.Generic;

namespace ExaminationSystem.Business.Abstract
{
    public interface IQuestionService
    {
        IResult AddQuestion(Question question, string userName);

        IResult UpdateQuestion(Question question, string userName);

        IResult DeleteQuestion(Question question);

        IDataResult<List<Question>> GetAll();

        IDataResult<List<Question>> GetExam(string categoryId);

        IDataResult<ExamResult> FinishExam(List<string> questionIds, List<string> userAnswers, UserWithIdDto user);

        IDataResult<List<Question>> GetByCategoryId(string categoryId);

        IDataResult<List<Question>> GetByUserName(string userId);

        IDataResult<Question> GetById(string id);
    }
}