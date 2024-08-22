using KibernumCrud.DataAccess.Configuration;
using KibernumCrud.DataAccess.Entities;
using KibernumCrud.DataAccess.Repositories.Interfaces;

namespace KibernumCrud.DataAccess.Repositories;

public sealed class UserRepository(KibernumCrudDbContext dbContext) : BaseRepository<User>(dbContext), IUserRepository;