using System.Linq.Expressions;
using GamesGlobal.Dal.Entities.Base;
using GamesGlobal.Dal.Persistence;

namespace GamesGlobal.Core.Repositories.Base;

public abstract class BaseRepository<T> where T : BaseEntity
{
    protected readonly IGeneralUnitOfWork _generalUnitOfWork;
    
    protected BaseRepository(IGeneralUnitOfWork generalUnitOfWork)
    {
        _generalUnitOfWork = generalUnitOfWork;
    }

    protected abstract IRepository<T> Repository { get; }
    
    public virtual Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression) => 
        Repository.Get(expression);

    public virtual Task<T?> GetById(long id) => Repository.GetById(id);

    public virtual Task Insert(T entity) => Repository.Insert(entity);

    public virtual Task Update(T entity) => Repository.Update(entity);

    public virtual async Task Upsert(T entity)
    {
        var foundEntity = await GetById(entity.Id);
        if (foundEntity == null)
            await Insert(entity);
        await Update(entity);
    }

    public virtual Task Delete(long id) => Repository.Delete(id);
    
    protected virtual Task ValidatedEntity(T entity) => Task.CompletedTask;
}