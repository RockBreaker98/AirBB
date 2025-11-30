using System.Collections.Generic;

namespace AirBB.Models.DataLayer.Repositories
{
    public interface IRepository<T> where T : class
    {
        T? Get(int id);
        T? Get(QueryOptions<T> options);

        IEnumerable<T> List(QueryOptions<T> options);

        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

        void Save();     // 👈 MUST be void (Murach style)
    }
}
