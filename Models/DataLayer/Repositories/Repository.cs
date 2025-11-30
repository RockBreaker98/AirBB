using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Models.DataLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AirBBContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AirBBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T? Get(int id) => _dbSet.Find(id);

        public T? Get(QueryOptions<T> options)
        {
            IQueryable<T> query = _dbSet;
            query = options.Apply(query);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> List(QueryOptions<T> options)
        {
            IQueryable<T> query = _dbSet;
            query = options.Apply(query);
            return query.ToList();
        }

        public void Insert(T entity) => _dbSet.Add(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public void Save() => _context.SaveChanges();  // 👈 void return
    }
}