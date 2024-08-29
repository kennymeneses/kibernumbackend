using System.Data.Common;
using System.Text.Json;
using FluentAssertions.Common;
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
    
    public static async Task<(DbConnection DbConnection, Respawner Respawner)> InitializeRespawner(IServiceProvider servicesProvider)
    {
        var kibernumCrudDbContext = servicesProvider.CreateScope().ServiceProvider.GetRequiredService<KibernumCrudDbContext>();
        var connection = kibernumCrudDbContext.Database.GetDbConnection();
        await connection.OpenAsync();
        Respawner respawner = await Respawner.CreateAsync(
            connection,
            new RespawnerOptions
            {
                DbAdapter = DbAdapter.Postgres,
                WithReseed = true
            });

        return (connection, respawner);
    }
    
    public static void UseLocalDatabase(
        IServiceCollection services,
        PostgreSqlContainer postgreSqlContainer)
    {
        services
            .AddDbContext<KibernumCrudDbContext>(options =>
            {
                options.UseNpgsql(postgreSqlContainer.GetConnectionString());
            });
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