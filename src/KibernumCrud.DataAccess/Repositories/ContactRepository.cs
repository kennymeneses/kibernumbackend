using KibernumCrud.DataAccess.Configuration;
using KibernumCrud.DataAccess.Entities;

namespace KibernumCrud.DataAccess.Repositories.Interfaces;

public sealed class ContactRepository(KibernumCrudDbContext dbContext) : BaseRepository<Contact>(dbContext), IContactRepository;