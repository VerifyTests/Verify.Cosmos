using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class Tests
{
    [Fact]
    public async Task CreateItem()
    {
        var client = new CosmosClient(
            "https://localhost:8081",
            "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
            new CosmosClientOptions
            {
                ApplicationName = "Verify.Cosmos"
            });

        Database database = await client.CreateDatabaseIfNotExistsAsync("db");

        Container container = await database.CreateContainerIfNotExistsAsync("items", "/LastName", 400);

        Family family = new()
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

        ItemResponse<Family>? response = await container.CreateItemAsync(family, new PartitionKey(family.LastName));
        Headers? responseHeaders = response.Headers;
        await Verifier.Verify(response);
    }
}