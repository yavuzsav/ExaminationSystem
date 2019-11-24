using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Models.Entities;
using System.Collections.Generic;

namespace ExaminationSystem.Business.Abstract
{
    public interface IClassLevelService
    {
        IResult Add(ClassLevel classLevel);

        IResult Update(ClassLevel classLevel);

        IResult Delete(ClassLevel classLevel);

        IDataResult<List<ClassLevel>> GetAll();

        IDataResult<ClassLevel> GetById(string id);

        //IDataResult<List<Category>> GetByCategories(string classLevelId);
    }
}