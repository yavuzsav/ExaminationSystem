using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Business.BusinessAspects.Autofac;
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
using ExaminationSystem.Framework.Utilities.Business;
using ExaminationSystem.Framework.Utilities.Helpers;
using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Framework.Utilities.Results.ErrorResults;
using ExaminationSystem.Framework.Utilities.Results.SuccessResults;
using ExaminationSystem.Framework.Utilities.Security.User;
using ExaminationSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExaminationSystem.Business.Concrete
{
    [AuthenticationAspect]
    [SecuredOperation("Admin")]
    [PerformanceAspect(10)]
    [LogAspect(typeof(DatabaseLogger))]
    [TransactionScopeAspect]
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IUserDal _userDal;
        private readonly IUserAccessor _userAccessor;

        public CategoryManager(ICategoryDal categoryDal, IUserDal userDal, IUserAccessor userAccessor)
        {
            _categoryDal = categoryDal;
            _userDal = userDal;
            _userAccessor = userAccessor;
        }

        [CacheAspect(120)]
        public IDataResult<List<Category>> GetList()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.Get().ToList());
        }

        [CacheAspect(120)]
        public IDataResult<Category> GetById(string categoryId)
        {
            return new SuccessDataResult<Category>(_categoryDal.Find(x => x.Id == categoryId));
        }

        [ValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect("Get")]
        public IResult Add(Category category)
        {
            IResult result = BusinessRules.Run(CheckIfCategoryExists(category.CategoryName, category.ClassLevelId));

            if (result != null)
            {
                return result;
            }

            category.Id = CreateUniqueId.CreateId();
            category.CreatedUser = _userDal.GetByUserName(_userAccessor.GetCurrentUserName());
            category.OnCreated = DateTime.Now;

            _categoryDal.Insert(category);
            return new SuccessResult(Messages.AddedSuccess);
        }

        [ValidationAspect(typeof(CategoryValidator))]
        [CacheRemoveAspect("Get")]
        public IResult Update(Category category)
        {
            IResult result = BusinessRules.Run(CheckIfCategoryExists(category.CategoryName, category.ClassLevelId, category.Id));

            if (result != null)
            {
                return result;
            }

            category.ModifiedUser = _userDal.GetByUserName(_userAccessor.GetCurrentUserName());
            category.OnModified = DateTime.Now;

            _categoryDal.Update(category);
            return new SuccessResult(Messages.UpdatedSuccess);
        }

        [CacheRemoveAspect("Get")]
        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult(Messages.DeletedSuccess);
        }

        [CacheAspect(120)]
        public IDataResult<List<Category>> GetCategoriesByClassLevel(string classLevelId)
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.Get(x => x.ClassLevelId == classLevelId).ToList());
        }

        private IResult CheckIfCategoryExists(string categoryName, string classLevelId, string id = null)
        {
            if (_categoryDal.GetList()
                .IsMultiDuplicate(x => x.CategoryName.ToLower() == categoryName.ToLower(), x => x.ClassLevelId.ToLower() == classLevelId.ToLower(), x => x.Id != id))
            {
                return new ErrorResult(CategoryMessages.CategoryAlreadyExists);
            }

            return new SuccessResult();
        }
    }
}