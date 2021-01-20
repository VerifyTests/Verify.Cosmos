using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class Tests
{
    static CosmosClient client = new(
        "https://localhost:8081",
        "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
        new CosmosClientOptions
        {
            ApplicationName = "Verify.Cosmos"
        });

    [Fact]
    public async Task CreateDatabase()
    {
        var database = await client.CreateDatabaseIfNotExistsAsync("db");
        await Verifier.Verify(database);
    }

    [Fact]
    public async Task CreateContainer()
    {
        Database database = await client.CreateDatabaseIfNotExistsAsync("db");
        Container container = await database.CreateContainerIfNotExistsAsync("items", "/LastName", 400);
        await Verifier.Verify(container);
    }

    [Fact]
    public async Task ItemResponse()
    {
        Database database = await client.CreateDatabaseIfNotExistsAsync("db");

        Container container = await database.CreateContainerIfNotExistsAsync("items", "/LastName", 400);

        Family item = new()
        {
            Id = Guid.NewGuid(),
            LastName = "Andersen",
            Address = new Address
            {
                State = "WA",
                County = "King",
                City = "Seattle"
            }
        };
        #region ItemResponse
        var response = await container.CreateItemAsync(
            item,
            new PartitionKey(item.LastName));
        await Verifier.Verify(response);
        #endregion
    }
}