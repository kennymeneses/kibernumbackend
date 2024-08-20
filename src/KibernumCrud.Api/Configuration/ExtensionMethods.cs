using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using KibernumCrud.Api.Configuration.Swagger;
using Constants = KibernumCrud.Api.Configuration.ConfigurationConstants;

namespace KibernumCrud.Api.Configuration;

public static class ExtensionMethods
{
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
}