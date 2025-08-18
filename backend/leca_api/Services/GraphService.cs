using Microsoft.Graph;
using Azure.Identity;

namespace leca_api.Services;

public interface IGraphService
{
    GraphServiceClient GetGraphServiceClient();
}

public class GraphService : IGraphService
{
    private readonly IConfiguration _configuration;
    private GraphServiceClient? _graphServiceClient;

    public GraphService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public GraphServiceClient GetGraphServiceClient()
    {
        if (_graphServiceClient != null)
            return _graphServiceClient;

        var clientId = _configuration["AzureAd:ClientId"];
        var clientSecret = _configuration["AzureAd:ClientSecret"];
        var tenantId = _configuration["AzureAd:TenantId"];

        if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret) || string.IsNullOrEmpty(tenantId))
        {
            throw new InvalidOperationException("Azure AD configuration is missing. Please check ClientId, ClientSecret, and TenantId in appsettings.json");
        }

        var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);
        _graphServiceClient = new GraphServiceClient(credential);

        return _graphServiceClient;
    }
}