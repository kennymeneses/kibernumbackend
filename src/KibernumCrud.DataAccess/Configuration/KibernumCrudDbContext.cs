using KibernumCrud.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace KibernumCrud.DataAccess.Configuration;

public class KibernumCrudDbContext : DbContext
{
    public KibernumCrudDbContext(DbContextOptions<KibernumCrudDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            user.HasKey(usr => usr.Id);
            user.HasOne(usr => usr.UserPassword).WithOne(up => up.User).HasForeignKey<UserPassword>(usr => usr.UserId);
        });

        modelBuilder.Entity<Contact>(contact =>
        {
            contact.HasKey(cntct => cntct.Id);
        });
    }
}