namespace KibernumCrud.Application.Models.V1.Security;

public sealed class JwtSettings
{
    public string Issuer { get; set; } = "somebody";
    public string Audience { get; set; } = "nobody";
    public string Key { get; set; } = string.Empty;
}