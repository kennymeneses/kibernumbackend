using KibernumCrud.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace KibernumCrud.IntegrationTests.Configuration;

public class KibernumApiFactory : WebApplicationFactory<IApiMarker>
{
    public HttpClient HttpClient { get; private set; }

    public KibernumApiFactory()
    {
        HttpClient = CreateClient();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(
            services =>
            {
                services.RemoveAll(typeof(IHostedService));
            });

        builder.ConfigureLogging(logging => logging.ClearProviders());

        builder.UseEnvironment(Constants.TestEnvName);
    }
}