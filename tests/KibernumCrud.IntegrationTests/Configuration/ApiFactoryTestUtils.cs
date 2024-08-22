using System.Data.Common;
using KibernumCrud.DataAccess.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Npgsql;
using Respawn;
using Respawn.Graph;
using Testcontainers.PostgreSql;
using WebMotions.Fake.Authentication.JwtBearer;

namespace KibernumCrud.IntegrationTests.Configuration;

public static class ApiFactoryTestUtils
{
    public static PostgreSqlContainer CreatePostgresContainer(
        string defaultDbUsernamePassword,
        bool enableTestDatabaseInspection,
        int defaultPostgresPort)
    {
        PostgreSqlBuilder? postgreSqlBuilder = new PostgreSqlBuilder()
            .WithDatabase(defaultDbUsernamePassword)
            .WithUsername(defaultDbUsernamePassword)
            .WithPassword(defaultDbUsernamePassword);

        if (enableTestDatabaseInspection)
        {
            postgreSqlBuilder = postgreSqlBuilder.WithPortBinding(defaultPostgresPort, defaultPostgresPort);
        }

        return postgreSqlBuilder.Build();
    }
    
    public static async Task MigrateDatabase(IServiceProvider services)
    {
        using IServiceScope scope = services.CreateScope();
        IServiceProvider scopedServices = scope.ServiceProvider;
        KibernumCrudDbContext db = scopedServices.GetRequiredService<KibernumCrudDbContext>();
        await db.Database.MigrateAsync();
    }
    
    public static async Task<(DbConnection DbConnection, Respawner Respawner)> InitializeRespawner(
        PostgreSqlContainer postgreSqlContainer,
        string KibernumSchema)
    {
        DbConnection dbConnection = new NpgsqlConnection(postgreSqlContainer.GetConnectionString());
        await dbConnection.OpenAsync();
        Respawner respawner = await Respawner.CreateAsync(
            dbConnection,
            new RespawnerOptions
            {
                DbAdapter = DbAdapter.Postgres,
                SchemasToInclude = [KibernumSchema],
                TablesToIgnore =
                [
                    new Table(
                        KibernumSchema,
                        "users"),
                    new Table(
                        KibernumSchema,
                        "userpaswords"),
                    new Table(
                        KibernumSchema,
                        "contacts")
                ],
                WithReseed = true
            });
        return (dbConnection, respawner);
    }
    
    public static void UseLocalDatabase(
        IServiceCollection services,
        PostgreSqlContainer postgreSqlContainer,
        bool enableTestDatabaseInspection)
    {
        services
            .RemoveAll<DbContextOptions<KibernumCrudDbContext>>()
            .AddDbContext<KibernumCrudDbContext>(
                (sp, options) =>
                {
                    if (enableTestDatabaseInspection)
                    {
                        options.LogTo(Console.WriteLine);
                    }

                    options.UseNpgsql(postgreSqlContainer.GetConnectionString() + ";Include Error Detail=true");
                    options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
                    options.EnableSensitiveDataLogging();
                },
                ServiceLifetime.Singleton,
                ServiceLifetime.Singleton);
        services.RemoveAll<IOptions<KibernumCrudDbContext>>();
    }

    public static void MockAuthentication(IServiceCollection services)
    {
        services
            .AddAuthentication(FakeJwtBearerDefaults.AuthenticationScheme)
            .AddFakeJwtBearer(
                options => options.ForwardDefaultSelector = ctx => FakeJwtBearerDefaults.AuthenticationScheme);
        
        services.PostConfigure<AuthenticationOptions>(options =>
        {
            options.DefaultAuthenticateScheme = FakeJwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = FakeJwtBearerDefaults.AuthenticationScheme;
        });
    }
}