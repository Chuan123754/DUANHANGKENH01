using appAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace appAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly APP_DATA_DATN _context;
        private readonly DbSet<T> _entities;

        public Repository(APP_DATA_DATN context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _entities;
        }

        public T GetById(long id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate).ToList();
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
            _context.SaveChanges();
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _entities.RemoveRange(entities);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetPaged(int pageIndex, int pageSize)
        {
            return _entities.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _entities.Where(predicate);

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.ToList();
        }

        public void UpdateStatus(long id, string status, DateTime updateAt)
        {
            var entity = _entities.Find(id);

            if (entity == null)
            {
                throw new Exception("Entity không tồn tại.");
            }

            // Cập nhật các trường cụ thể
            var propertyStatus = typeof(T).GetProperty("Status");
            var propertyUpdateAt = typeof(T).GetProperty("Update_at");

            if (propertyStatus != null)
            {
                propertyStatus.SetValue(entity, status);
            }

            if (propertyUpdateAt != null)
            {
                propertyUpdateAt.SetValue(entity, updateAt);
            }

            // Lưu thay đổi
            _context.SaveChanges();
        }

        public async Task BatchUpdateAsync(IEnumerable<T> entities)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                foreach (var entity in entities)
                {
                    _context.Set<T>().Update(entity);
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
    }
}
