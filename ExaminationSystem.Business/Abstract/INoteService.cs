using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Models.Entities;
using System.Collections.Generic;
using ExaminationSystem.Framework.Aspects.Autofac.Transaction;

namespace ExaminationSystem.Business.Abstract
{
    public interface INoteService
    {
        [TransactionScopeAspect]
        IResult AddNote(Note note);

        [TransactionScopeAspect]
        IResult UpdateNote(Note note);

        [TransactionScopeAspect]
        IResult DeleteNote(Note note);

        IDataResult<List<Note>> GetAll();

        IDataResult<List<Note>> GetByUserId(string userId);

        IDataResult<List<Note>> GetByCategoryId(string categoryId);
    }
}