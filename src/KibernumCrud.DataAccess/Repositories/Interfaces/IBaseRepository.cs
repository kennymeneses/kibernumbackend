using System.Linq.Expressions;
using KibernumCrud.DataAccess.Entities;

namespace KibernumCrud.DataAccess.Repositories.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<int> CountAsync(CancellationToken cancellationToken);
    Task<List<TEntity>> ListAsync(CancellationToken cancellationToken);
    Task<TEntity?> GetById(int id, CancellationToken cancellationToken);
    Task<TEntity?> FirstOrDefaultAsync<TProperty>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task CreateAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}