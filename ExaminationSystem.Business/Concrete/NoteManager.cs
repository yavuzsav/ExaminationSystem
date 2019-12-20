using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Business.Constants;
using ExaminationSystem.DataAccess.Abstract;
using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Framework.Utilities.Results.SuccessResults;
using ExaminationSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using ExaminationSystem.Business.BusinessAspects.Autofac;

namespace ExaminationSystem.Business.Concrete
{
    [AuthenticationAspect]
    public class NoteManager : INoteService
    {
        private readonly INoteDal _noteDal;

        public NoteManager(INoteDal noteDal)
        {
            _noteDal = noteDal;
        }

        [SecuredOperation("Admin")]
        public IResult AddNote(Note note)
        {
            note.Date = DateTime.Now;
            _noteDal.Insert(note);
            return new SuccessResult(Messages.AddedSuccess);
        }

        [SecuredOperation("Admin")]
        public IResult UpdateNote(Note note)
        {
            _noteDal.Update(note);
            return new SuccessResult(Messages.UpdatedSuccess);
        }

        [SecuredOperation("Admin")]
        public IResult DeleteNote(Note note)
        {
            _noteDal.Delete(note);
            return new SuccessResult(Messages.DeletedSuccess);
        }

        [SecuredOperation("Admin")]
        public IDataResult<List<Note>> GetAll()
        {
            return new SuccessDataResult<List<Note>>(_noteDal.Get().OrderByDescending(x => x.Date).ToList());
        }

        [SecuredOperation("Admin,Student")]
        public IDataResult<List<Note>> GetByUserId(string userId)
        {
            return new SuccessDataResult<List<Note>>(_noteDal.Get(x => x.UserId == userId).ToList());
        }

        [SecuredOperation("Admin")]
        public IDataResult<List<Note>> GetByCategoryId(string categoryId)
        {
            return new SuccessDataResult<List<Note>>(_noteDal.Get(x => x.CategoryId == categoryId).ToList());
        }
    }
}