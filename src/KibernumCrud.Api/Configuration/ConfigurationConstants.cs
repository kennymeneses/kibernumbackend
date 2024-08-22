namespace KibernumCrud.Api.Configuration;

public class ConfigurationConstants
{
    #region SwaggerValuesConfiguration
    
    public const int DefaultApiVersion = 1;
    public const int MinorApiVersion = 0;
    public const string GroupNameFormat = "'v'VVV";
    public const string SwaggerJsonEndpoint = "/swagger/{0}/swagger.json";
    public const string ApiTitle = "Kibernum - Crud API .NET 8 - Code Challenge";

    #endregion
    
    public const string ConnectionStringDb = "kibernum_connectionstring";
    public const string KeySignature = "kibernum_signature_key";

    public const string IamSectionName = "IAM";
    public const string AccessKeyName = "ak";
    public const string SecureAccessKeyName = "sak";

    public const string ApiPolicyName = "apiPolicy";
}