using GamesGlobal.Dal.Entities.Base;

namespace GamesGlobal.Core.Repositories.Base;

public interface IReadWriteRepository<T> : IReadOnlyRepository<T> where T : BaseEntity
{
    Task<T> Insert(T entity);
    Task<T> Update(T entity);
    Task<T> Upsert(T entity);
    Task Delete(long id);
}