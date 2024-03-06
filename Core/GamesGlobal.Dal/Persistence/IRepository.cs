using System.Linq.Expressions;
using GamesGlobal.Dal.Entities.Base;

namespace GamesGlobal.Dal.Persistence;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetById(long id);
    Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);
    Task Insert(T entity);
    Task Update(T entity);
    Task Delete(long id);
}