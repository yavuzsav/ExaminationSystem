﻿using ExaminationSystem.DataAccess.Abstract;
using ExaminationSystem.DataAccess.Concrete.EntityFramework.Contexts;
using ExaminationSystem.Framework.Infrastructure;
using ExaminationSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.DataAccess.Concrete.EntityFramework
{
    public class EfSolvedQuestionDal : EfRepositoryBase<SolvedQuestion>, ISolvedQuestionDal
    {
        public EfSolvedQuestionDal(ExaminationSystemContext context) : base(context)
        {
        }
    }
}