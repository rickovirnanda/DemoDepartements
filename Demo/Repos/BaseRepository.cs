using Demo.Contracts;
using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Demo.Repos
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DemoContext Context;

        public BaseRepository(DemoContext context) =>
            Context = context;

        public virtual bool Create(T entity)
        {
            try
            {
                entity.IsActive = true;
                entity.IsDeleted = false;

                entity.CreatedBy = "Actor";
                entity.CreatedDate = DateTime.Now;

                entity.ModifiedBy = "Actor";
                entity.ModifiedDate = DateTime.Now;

                Context.Set<IEntity>().Add(entity);
                Context.SaveChanges();

                return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public virtual bool Delete(T entity)
        {
            try
            {
                entity.IsDeleted = true;

                entity.ModifiedBy = "Actor";
                entity.ModifiedDate = DateTime.Now;

                Context.Set<IEntity>().Update(entity);
                Context.SaveChanges();

                return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public virtual T GetById(long id) =>
            Context.Set<T>().Find(id);

        public virtual IEnumerable<T> GetEntities() =>
            Context.Set<T>().AsEnumerable();

        public virtual IEnumerable<T> GetEntities(Expression<Func<T, bool>> predicate) =>
            Context.Set<T>().Where(predicate)
                            .AsEnumerable();

        public virtual bool Update(T entity)
        {
            try
            {
                entity.IsDeleted = false;

                entity.ModifiedBy = "Actor";
                entity.ModifiedDate = DateTime.Now;

                Context.Set<IEntity>().Update(entity);
                Context.SaveChanges();

                return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}