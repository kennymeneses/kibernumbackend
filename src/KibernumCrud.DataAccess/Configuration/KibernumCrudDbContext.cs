using KibernumCrud.DataAccess.Configuration.Interfaces;
using KibernumCrud.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace KibernumCrud.DataAccess.Configuration;

public class KibernumCrudDbContext : DbContext, IUnitOfWork
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
            contact.HasOne(cntct => cntct.User).WithMany(user => user.Contacts).HasForeignKey(cntct => cntct.UserId);
        });
    }
    
    public async Task CommitChangesAsync(CancellationToken cancellationToken)
    {
        await SaveChangesAsync(cancellationToken);
    }
    
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Contact> Contacts { get; set; }
    public virtual DbSet<UserPassword> UserPasswords { get; set; }
}