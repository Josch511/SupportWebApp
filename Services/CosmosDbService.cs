using Microsoft.Azure.Cosmos;
using SupportWebApp.Models;

namespace SupportWebApp.Services;

public class CosmosDbService
{
    private readonly Container _container;

    public CosmosDbService(IConfiguration configuration)
    {
        var connectionString =
            configuration["CosmosDb:ConnectionString"]
            ?? throw new InvalidOperationException(
                "Cosmos DB connection string is missing.");

        var databaseName =
            configuration["CosmosDb:DatabaseName"]
            ?? throw new InvalidOperationException(
                "Cosmos DB database name is missing.");

        var containerName =
            configuration["CosmosDb:ContainerName"]
            ?? throw new InvalidOperationException(
                "Cosmos DB container name is missing.");

        var client = new CosmosClient(
            connectionString,
            new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions
                {
                    PropertyNamingPolicy =
                        CosmosPropertyNamingPolicy.CamelCase
                }
            });

        _container = client.GetContainer(databaseName, containerName);
    }

    public async Task AddAsync(SupportMessage message)
    {
        await _container.CreateItemAsync(
            message,
            new PartitionKey(message.Category));
    }

    public async Task<List<SupportMessage>> GetAllAsync()
    {
        var messages = new List<SupportMessage>();

        var query = _container.GetItemQueryIterator<SupportMessage>(
            "SELECT * FROM c ORDER BY c.createdAt DESC");

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            messages.AddRange(response);
        }

        return messages;
    }
}