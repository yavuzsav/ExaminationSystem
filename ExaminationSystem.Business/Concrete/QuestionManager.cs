using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Business.BusinessAspects.Autofac;
using ExaminationSystem.Business.Constants;
using ExaminationSystem.Business.Helpers;
using ExaminationSystem.Business.ValidationRules.FluentValidation;
using ExaminationSystem.DataAccess.Abstract;
using ExaminationSystem.Framework.Aspects.Autofac.Caching;
using ExaminationSystem.Framework.Aspects.Autofac.Logging;
using ExaminationSystem.Framework.Aspects.Autofac.Performance;
using ExaminationSystem.Framework.Aspects.Autofac.Transaction;
using ExaminationSystem.Framework.Aspects.Autofac.Validation;
using ExaminationSystem.Framework.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using ExaminationSystem.Framework.Utilities.Helpers;
using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Framework.Utilities.Results.ErrorResults;
using ExaminationSystem.Framework.Utilities.Results.SuccessResults;
using ExaminationSystem.Framework.Utilities.Security.User;
using ExaminationSystem.Models.Dtos.User;
using ExaminationSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExaminationSystem.Business.Concrete
{
    [AuthenticationAspect]
    [PerformanceAspect(10)]
    [LogAspect(typeof(DatabaseLogger))]
    [TransactionScopeAspect]
    public class QuestionManager : IQuestionService
    {
        private readonly IQuestionDal _questionDal;
        private readonly IExamParameterService _examParameterService;
        private readonly INoteService _noteService;
        private readonly IUserService _userService;
        private readonly IUserDal _userDal;
        private readonly IUserAccessor _userAccessor;

        public QuestionManager(IQuestionDal questionDal, IExamParameterService examParameterService, INoteService noteService, IUserService userService, IUserDal userDal, IUserAccessor userAccessor)
        {
            _questionDal = questionDal;
            _examParameterService = examParameterService;
            _noteService = noteService;
            _userService = userService;
            _userDal = userDal;
            _userAccessor = userAccessor;
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(QuestionValidator))]
        [CacheRemoveAspect("Get")]
        public IResult AddQuestion(Question question, string userName)
        {
            question.Id = CreateUniqueId.CreateId();
            question.CreatedUser = _userDal.GetByUserName(_userAccessor.GetCurrentUserName());
            question.OnCreated = DateTime.Now;

            _questionDal.Insert(question);
            return new SuccessResult(Messages.AddedSuccess);
        }

        [SecuredOperation("Admin")]
        [ValidationAspect(typeof(QuestionValidator))]
        [CacheRemoveAspect("Get")]
        public IResult UpdateQuestion(Question question, string userName)
        {
            question.ModifiedUser = _userDal.GetByUserName(_userAccessor.GetCurrentUserName());
            question.OnModified = DateTime.Now;

            _questionDal.Update(question);
            return new SuccessResult(Messages.UpdatedSuccess);
        }

        [SecuredOperation("Admin")]
        [CacheRemoveAspect("Get")]
        public IResult DeleteQuestion(Question question)
        {
            _questionDal.Delete(question);
            return new SuccessResult(Messages.DeletedSuccess);
        }

        [SecuredOperation("Admin")]
        [CacheAspect(120)]
        public IDataResult<List<Question>> GetAll()
        {
            return new SuccessDataResult<List<Question>>(_questionDal.Get().ToList());
        }

        [SecuredOperation("Admin,Student")]
        public IDataResult<List<Question>> GetExam(string categoryId)
        {
            var parameter = _examParameterService.GetParameter().Data;
            var questionList = _questionDal.Get(x => x.CategoryId == categoryId).OrderBy(x => Guid.NewGuid()).ToList();
            parameter ??= new ExamParameter();

            if (questionList.Count < parameter.NumberOfQuestions)
            {
                return new ErrorDataResult<List<Question>>(Messages.NotEnoughQuestions);
            }

            var exam = questionList.Take(parameter.NumberOfQuestions).ToList();

            return new SuccessDataResult<List<Question>>(exam);

            //todo daha önce çözdüğü soruyu bir daha çekmesin
        }

        [SecuredOperation("Admin,Student")]
        public IDataResult<ExamResult> FinishExam(List<string> questionIds, List<string> userAnswers, UserWithIdDto user)
        {
            int correct = 0;
            int wrong = 0;
            int empty = 0;
            var questions = new List<Question>();

            foreach (var questionId in questionIds)
            {
                var data = GetById(questionId).Data;
                if (data != null)
                    questions.Add(data);
            }

            if (questions.Count != userAnswers.Count)
            {
                return new ErrorDataResult<ExamResult>(QuestionMessages.UserAnswerCountNotEqualQuestionCount);
            }

            for (int i = 0; i < questions.Count; i++)
            {
                if (userAnswers[i] == null)
                {
                    empty += 1;
                }
                else if (questions[i].CorrectAnswer.ToString().ToLower() == userAnswers[i].ToLower())
                {
                    correct += 1;
                }
                else
                {
                    wrong += 1;
                }
            }

            _noteService.AddNote(new Note
            {
                Id = CreateUniqueId.CreateId(),
                CategoryId = questions[0].CategoryId,
                Correct = correct,
                Wrong = wrong,
                Empty = empty,
                UserId = user.Id
            });

            return new SuccessDataResult<ExamResult>(new ExamResult
            {
                Correct = correct,
                Wrong = wrong,
                Empty = empty
            });
        }

        [SecuredOperation("Admin")]
        [CacheAspect(120)]
        public IDataResult<List<Question>> GetByCategoryId(string categoryId)
        {
            return new SuccessDataResult<List<Question>>(_questionDal.Get(x => x.CategoryId == categoryId).ToList());
        }

        [SecuredOperation("Admin")]
        [CacheAspect(120)]
        public IDataResult<List<Question>> GetByUserName(string userName)
        {
            return new SuccessDataResult<List<Question>>(_questionDal.Get(x => x.CreatedUser.UserName == userName).ToList());
        }

        [SecuredOperation("Admin")]
        [CacheAspect(120)]
        public IDataResult<Question> GetById(string id)
        {
            return new SuccessDataResult<Question>(_questionDal.Find(x => x.Id == id));
        }
    }
}