using ExaminationSystem.Framework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExaminationSystem.Framework.Infrastructure
{
    public class EfRepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EfRepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public void Insert(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                _context.Entry(entity).State = EntityState.Added;

            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Update(TEntity entity, IEnumerable<string> fields)
        {
            _context.Set<TEntity>().Attach(entity);
            var entry = _context.Entry(entity);
            foreach (var field in fields)
                entry.Property(field).IsModified = true;

            _context.SaveChanges();
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;

            _context.SaveChanges();
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                _context.Entry(entity).State = EntityState.Deleted;

            _context.SaveChanges();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> filter)
        {
            return filter == null ? _dbSet.FirstOrDefault() : _dbSet.Where(filter).FirstOrDefault();
        }

        public TResult Find<TResult>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TResult>> selector)
        {
            return filter == null ? _dbSet.Select(selector).FirstOrDefault() : _dbSet.Where(filter).Select(selector).FirstOrDefault();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? _dbSet : _dbSet.Where(filter);
        }

        public IQueryable<TResult> Get<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? _dbSet.Select(selector) : _dbSet.Where(filter).Select(selector);
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? _dbSet.ToList() : _dbSet.Where(filter).ToList();
        }

        public IList<TResult> GetList<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? _dbSet.Select(selector).ToList() : _dbSet.Where(filter).Select(selector).ToList();
        }

        public int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? _dbSet.Count() : _dbSet.Count(filter);
        }
    }
}