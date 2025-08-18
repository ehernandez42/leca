using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;


namespace leca_api;

public class GraphHandler
{
    public GraphServiceClient GraphServiceClient;

    public GraphHandler(string tenantId, string clientId, string clientSecret)
    {
        GraphServiceClient = CreateGraphClient(tenantId, clientId, clientSecret);
    }

    public GraphServiceClient CreateGraphClient(string tenantId, string clientId, string clientSecret)
    {
        var options = new TokenCredentialOptions
        {
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
        };
        
        var clientSecretCredential = new ClientSecretCredential(
            tenantId, clientId, clientSecret, options);
        var scopes = new[] {"https://graph.microsoft.com/.default"};
        return new GraphServiceClient(clientSecretCredential, scopes);
    }

    public async Task<User?> GetUser(string userPrincipalName)
    {
        return await GraphServiceClient.Users[userPrincipalName].GetAsync();
    }
}