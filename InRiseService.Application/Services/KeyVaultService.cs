using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace InRiseService.Application.Services
{
    public class KeyVaultService
    {
        private readonly SecretClient _secretClient;

        public KeyVaultService()
        {
            string  _keyVaultName = Environment.GetEnvironmentVariable("SECRET_NAME");
            string kvUri = $"https://{_keyVaultName}.vault.azure.net";
            _secretClient = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            try
            {
                KeyVaultSecret secret = await _secretClient.GetSecretAsync(secretName);
                return secret.Value;
            }
            catch (Azure.RequestFailedException ex)
            {
                // Handle exceptions as needed, such as logging or re-throwing
                throw new InvalidOperationException($"Could not retrieve the secret: {secretName}", ex);
            }
        }
    }
}