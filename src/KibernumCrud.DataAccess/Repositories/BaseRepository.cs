using System.Linq.Expressions;
using KibernumCrud.DataAccess.Configuration;
using KibernumCrud.DataAccess.Entities;
using KibernumCrud.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KibernumCrud.DataAccess.Repositories;

public abstract class BaseRepository<TEntity>: IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly KibernumCrudDbContext _dbContext;

    public BaseRepository(KibernumCrudDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public virtual async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Set<TEntity>().CountAsync(cancellationToken);
    }

    public async Task<List<TEntity>> ListAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.FindAsync<TEntity>(id);
    }

    public async Task<TEntity?> FirstOrDefaultAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task CreateAsync(TEntity entity)
    {
        entity.Uuid = Guid.NewGuid();
        entity.CreationDate = DateTime.Now.ToUniversalTime();
        entity.Deleted = false;

        await _dbContext.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        entity.Deleted = true;
        _dbContext.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        entity.Deleted = true;
        _dbContext.Update(entity);
    }
}