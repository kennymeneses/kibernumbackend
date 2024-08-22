using KibernumCrud.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
await builder.Services.AddSignatureKey(builder.Configuration);
await builder.Services.AddSqlService(builder.Configuration);
builder.Services.ConfigureMediator();
builder.Services.AddApiAuthentication();
builder.Services.AddRepositories();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerVersioning();

builder.Services.AddCorsPolicy();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.AddSwaggerUiConfiguration();
}

app.UseHttpsRedirection();

app.UseCors(ConfigurationConstants.ApiPolicyName);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
