using System.Linq.Expressions;
using GamesGlobal.Dal.Entities.Base;

namespace GamesGlobal.Core.Repositories.Base;

public interface IReadOnlyRepository<T> where T : BaseEntity
{
    Task<T?> GetById(long id);
    Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression);
}