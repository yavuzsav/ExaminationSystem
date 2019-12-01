using ExaminationSystem.Business.Abstract;
using ExaminationSystem.Business.Constants;
using ExaminationSystem.DataAccess.Abstract;
using ExaminationSystem.Framework.Utilities.Results.BaseResults;
using ExaminationSystem.Framework.Utilities.Results.SuccessResults;
using ExaminationSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExaminationSystem.Business.Concrete
{
    public class NoteManager : INoteService
    {
        private readonly INoteDal _noteDal;

        public NoteManager(INoteDal noteDal)
        {
            _noteDal = noteDal;
        }

        public IResult AddNote(Note note)
        {
            note.Date = DateTime.Now;
            _noteDal.Insert(note);
            return new SuccessResult(Messages.AddedSuccess);
        }

        public IResult UpdateNote(Note note)
        {
            _noteDal.Update(note);
            return new SuccessResult(Messages.UpdatedSuccess);
        }

        public IResult DeleteNote(Note note)
        {
            _noteDal.Delete(note);
            return new SuccessResult(Messages.DeletedSuccess);
        }

        public IDataResult<List<Note>> GetAll()
        {
            return new SuccessDataResult<List<Note>>(_noteDal.Get().ToList());
        }

        public IDataResult<List<Note>> GetByUserId(string userId)
        {
            return new SuccessDataResult<List<Note>>(_noteDal.Get(x => x.UserId == userId).ToList());
        }

        public IDataResult<List<Note>> GetByCategoryId(string categoryId)
        {
            return new SuccessDataResult<List<Note>>(_noteDal.Get(x => x.CategoryId == categoryId).ToList());
        }
    }
}