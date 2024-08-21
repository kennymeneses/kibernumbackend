namespace KibernumCrud.DataAccess.Configuration.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync(CancellationToken cancellationToken);
}