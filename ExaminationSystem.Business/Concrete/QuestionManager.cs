using ExaminationSystem.Business.Abstract;
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
using ExaminationSystem.Models.Entities;
using ExaminationSystem.Models.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using ExaminationSystem.Models.Dtos.User;

namespace ExaminationSystem.Business.Concrete
{
    [PerformanceAspect(10)]
    [LogAspect(typeof(DatabaseLogger))]
    [TransactionScopeAspect]
    public class QuestionManager : IQuestionService
    {
        private readonly IQuestionDal _questionDal;
        private readonly IExamParameterService _examParameterService;
        private readonly INoteService _noteService;
        private readonly IUserService _userService;

        public QuestionManager(IQuestionDal questionDal, IExamParameterService examParameterService, INoteService noteService, IUserService userService)
        {
            _questionDal = questionDal;
            _examParameterService = examParameterService;
            _noteService = noteService;
            _userService = userService;
        }

        [ValidationAspect(typeof(QuestionValidator))]
        [CacheRemoveAspect("IQuestionService.Get")]
        public IResult AddQuestion(Question question, string userName)
        {
            question.Id = CreateUniqueId.CreateId();
            question.CreatedUserName = userName;
            question.OnCreated = DateTime.Now;

            _questionDal.Insert(question);
            return new SuccessResult(Messages.AddedSuccess);
        }

        [ValidationAspect(typeof(QuestionValidator))]
        [CacheRemoveAspect("IQuestionService.Get")]
        public IResult UpdateQuestion(Question question, string userName)
        {
            question.ModifiedUserName = userName;
            question.OnModified = DateTime.Now;

            _questionDal.Update(question);
            return new SuccessResult(Messages.UpdatedSuccess);
        }

        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult DeleteQuestion(Question question)
        {
            _questionDal.Delete(question);
            return new SuccessResult(Messages.DeletedSuccess);
        }

        [CacheAspect(120)]
        public IDataResult<List<Question>> GetAll()
        {
            return new SuccessDataResult<List<Question>>(_questionDal.Get().ToList());
        }

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

        [CacheAspect(120)]
        public IDataResult<List<Question>> GetByCategoryId(string categoryId)
        {
            return new SuccessDataResult<List<Question>>(_questionDal.Get(x => x.CategoryId == categoryId).ToList());
        }

        [CacheAspect(120)]
        public IDataResult<List<Question>> GetByUserName(string userName)
        {
            return new SuccessDataResult<List<Question>>(_questionDal.Get(x => x.CreatedUserName == userName).ToList());
        }

        [CacheAspect(120)]
        public IDataResult<Question> GetById(string id)
        {
            return new SuccessDataResult<Question>(_questionDal.Find(x => x.Id == id));
        }
    }
}