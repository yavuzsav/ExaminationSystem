using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Models.Entities;
using System.Collections.Generic;

namespace ExaminationSystem.Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetList();

        IDataResult<Category> GetById(string categoryId);

        IResult Add(Category category, string userName);

        IResult Update(Category category, string userName);

        IResult Delete(Category category);

        IDataResult<List<Category>> GetCategoriesByClassLevel(string classLevelId);
    }
}