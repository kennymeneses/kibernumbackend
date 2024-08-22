using System.Text;
using Amazon.Runtime;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using KibernumCrud.Api.Configuration.Secrets;
using KibernumCrud.Api.Configuration.Swagger;
using KibernumCrud.Application.Configuration;
using KibernumCrud.Application.Models.V1.Security;
using KibernumCrud.DataAccess.Configuration;
using KibernumCrud.DataAccess.Configuration.Interfaces;
using KibernumCrud.DataAccess.Repositories;
using KibernumCrud.DataAccess.Repositories.Interfaces;
using Mediator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Constants = KibernumCrud.Api.Configuration.ConfigurationConstants;

namespace KibernumCrud.Api.Configuration;

public static class ExtensionMethods
{
    public static async Task AddSqlService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork>(servicesProvider => servicesProvider.GetRequiredService<KibernumCrudDbContext>());
        
        string connectionString = await SecretHandler.GetSecret(GetCredentials(configuration), ConfigurationConstants.ConnectionStringDb);
        services.AddDbContext<KibernumCrudDbContext>(options => options.UseNpgsql(connectionString));
    }

    public static void ConfigureMediator(this IServiceCollection services)
    {
        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
    }

    public static async Task AddSignatureKey(this IServiceCollection services, IConfiguration configuration)
    {
        string signatureKey = await SecretHandler.GetSecret(GetCredentials(configuration), ConfigurationConstants.KeySignature);
        JwtSettings jwtSettings = new JwtSettings { Key = signatureKey };
        services.AddSingleton(jwtSettings);
    }
    
    public static void AddSwaggerVersioning(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        services.ConfigureOptions<ConfigureSwaggerOptions>();
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(Constants.DefaultApiVersion, Constants.MinorApiVersion);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = Constants.GroupNameFormat;
            options.SubstituteApiVersionInUrl = true;
        });
    }
    
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IUserPasswordRepository, UserPasswordRepository>();
    }

    public static void AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: ConfigurationConstants.ApiPolicyName,
                policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
        });
    }

    public static void AddApiAuthentication(this IServiceCollection services)
    {
        var sp = services.BuildServiceProvider();
        JwtSettings jwtSettings = sp.GetRequiredService<JwtSettings>();
        
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        });
    }
    
    public static void AddSwaggerUiConfiguration(this WebApplication app)
    {
        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(string.Format(Constants.SwaggerJsonEndpoint, description.GroupName),
                    description.GroupName.ToUpperInvariant());
            }
        });
    }

    private static BasicAWSCredentials GetCredentials(IConfiguration configuration)
    {
        string? accessKey = configuration.GetSection(ConfigurationConstants.IamSectionName)[ConfigurationConstants.AccessKeyName];
        string? secretKey = configuration.GetSection(ConfigurationConstants.IamSectionName)[ConfigurationConstants.SecureAccessKeyName];
        
        var credentials = new BasicAWSCredentials(accessKey, secretKey);
        
        return credentials;
    }
}