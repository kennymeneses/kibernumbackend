using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace KibernumCrud.Api.Configuration.Secrets;

public static class SecretHandler
{
    public static async Task<string> GetSecret(AWSCredentials credentials, string secretName)
    {
        string region = "us-east-2";
        
        IAmazonSecretsManager client = new AmazonSecretsManagerClient(credentials, RegionEndpoint.GetBySystemName(region));
        
        GetSecretValueRequest request = new GetSecretValueRequest
        {
            SecretId = secretName,
            VersionStage = "AWSCURRENT",
        };
        
        GetSecretValueResponse response = await client.GetSecretValueAsync(request);
        
        string secret = response.SecretString;
        
        return secret;
    }
}