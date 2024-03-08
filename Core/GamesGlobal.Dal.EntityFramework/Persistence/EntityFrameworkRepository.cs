using System.Linq.Expressions;
using GamesGlobal.Dal.Entities.Base;
using GamesGlobal.Dal.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GamesGlobal.Dal.EntityFramework.Persistence;

public class EntityFrameworkRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly GamesGlobalContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public EntityFrameworkRepository(GamesGlobalContext context, DbSet<T> dbSet)
    {
        _dbContext = context;
        _dbSet = dbSet;
    }
    
    public async Task<T?> GetById(long id) => await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> expression) =>
        await _dbSet.Where(expression).ToListAsync();

    public async Task<T?> GetOne(Expression<Func<T, bool>> expression) =>
        await _dbSet.FirstOrDefaultAsync(expression);

    public IQueryable<T> GetQueryable(Expression<Func<T, bool>> expression) =>
        _dbSet.Where(expression);

    public async Task<T> Insert(T entity)
    {
        entity.CreatedAt = DateTime.Now;
        
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<T> Update(T entity)
    {
        entity.LastUpdatedAt = DateTime.Now;
        
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task Delete(long id)
    {
        var entity = await GetById(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}