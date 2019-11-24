using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Business.Constants;
using ExaminationSystem.Business.ValidationRules.FluentValidation;
using ExaminationSystem.DataAccess.Abstract;
using ExaminationSystem.Framework.Aspects.Autofac.Caching;
using ExaminationSystem.Framework.Aspects.Autofac.Logging;
using ExaminationSystem.Framework.Aspects.Autofac.Performance;
using ExaminationSystem.Framework.Aspects.Autofac.Transaction;
using ExaminationSystem.Framework.Aspects.Autofac.Validation;
using ExaminationSystem.Framework.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using ExaminationSystem.Framework.Extensions;
using ExaminationSystem.Framework.Utilities.Helpers;
using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Framework.Utilities.Results.ErrorResults;
using ExaminationSystem.Framework.Utilities.Results.SuccessResults;
using ExaminationSystem.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ExaminationSystem.Business.Concrete
{
    [PerformanceAspect(10)]
    [LogAspect(typeof(DatabaseLogger))]
    [TransactionScopeAspect]
    public class ClassLevelManager : IClassLevelService
    {
        private readonly IClassLevelDal _classLevelDal;

        public ClassLevelManager(IClassLevelDal classLevelDal)
        {
            _classLevelDal = classLevelDal;
        }

        [ValidationAspect(typeof(ClassLevelValidator))]
        [CacheRemoveAspect("IClassLevelService.Get")]
        public IResult Add(ClassLevel classLevel)
        {
            //var isDuplicate = _classLevelDal.Get().IsDuplicate(x => x.ClassLevelName, classLevel.ClassLevelName);
            classLevel.Id = CreateUniqueId.CreateId();

            _classLevelDal.Insert(classLevel);
            return new SuccessResult(Messages.AddedSuccess);
        }

        [ValidationAspect(typeof(ClassLevelValidator))]
        [CacheRemoveAspect("IClassLevelService.Get")]
        public IResult Update(ClassLevel classLevel)
        {
            //var isDuplicate = _classLevelDal.Get().IsDuplicate(x => x.ClassLevelName, classLevel.ClassLevelName);

            _classLevelDal.Update(classLevel);
            return new SuccessResult(Messages.UpdatedSuccess);
        }

        [CacheRemoveAspect("IClassLevelService.Get")]
        public IResult Delete(ClassLevel classLevel)
        {
            _classLevelDal.Delete(classLevel);
            return new SuccessResult(Messages.DeletedSuccess);
        }

        [CacheAspect(3)]
        public IDataResult<List<ClassLevel>> GetAll()
        {
            return new SuccessDataResult<List<ClassLevel>>(_classLevelDal.Get().ToList());
        }

        [CacheAspect(3)]
        public IDataResult<ClassLevel> GetById(string id)
        {
            return new SuccessDataResult<ClassLevel>(_classLevelDal.Find(x => x.Id == id));
        }

        //public IDataResult<List<Category>> GetByCategories(string classLevelId)
        //{
        //    return new SuccessDataResult<List<Category>>(_classLevelDal.Find(x => x.Id == classLevelId).Categories.ToList());
        //}
    }
}