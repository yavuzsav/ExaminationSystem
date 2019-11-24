using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ExaminationSystem.Framework.Entities;

namespace ExaminationSystem.Framework.Infrastructure
{
    public interface IRepositoryBase<TEntity> where TEntity : class, IEntity, new()
    {
        void Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(TEntity entity, IEnumerable<string> fields);

        void Update(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        TEntity Find(Expression<Func<TEntity, bool>> filter);

        TResult Find<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null);

        IQueryable<TResult> Get<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> filter = null);

        IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null);

        IList<TResult> GetList<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null);

        int Count(Expression<Func<TEntity, bool>> filter = null);
    }
}