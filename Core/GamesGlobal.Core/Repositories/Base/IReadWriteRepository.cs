using GamesGlobal.Dal.Entities.Base;

namespace GamesGlobal.Core.Repositories.Base;

public interface IReadWriteRepository<T> : IReadOnlyRepository<T> where T : BaseEntity
{
    Task Insert(T entity);
    Task Update(T entity);
    Task Upsert(T entity);
    Task Delete(long id);
}