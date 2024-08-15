public class Tests
{
    static CosmosClient client = new(
        "https://localhost:8081",
        "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
        new()
        {
            ApplicationName = "Verify.Cosmos"
        });

    [Fact]
    public async Task DatabaseResponse()
    {
        var database = await client.CreateDatabaseIfNotExistsAsync("db");
        await Verify(database);
    }

    [Fact]
    public async Task ContainerResponse()
    {
        Database database = await client.CreateDatabaseIfNotExistsAsync("db");
        var container = await database.CreateContainerIfNotExistsAsync("items", "/LastName", 400);
        await Verify(container);
    }

    [Fact]
    public async Task ItemResponse()
    {
        Database database = await client.CreateDatabaseIfNotExistsAsync("db");

        Container container = await database.CreateContainerIfNotExistsAsync("items", "/LastName", 400);

        var item = new Family
        {
            Id = Guid.NewGuid().ToString(),
            LastName = "Andersen",
            Address = new()
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
        await Verify(response);

        #endregion
    }

    [Fact]
    public async Task FeedResponse()
    {
        Database database = await client.CreateDatabaseIfNotExistsAsync("db");

        Container container = await database.CreateContainerIfNotExistsAsync("items", "/LastName", 400);

        var item = new Family
        {
            Id = Guid.NewGuid().ToString(),
            LastName = "Andersen",
            Address = new()
            {
                State = "WA",
                County = "King",
                City = "Seattle"
            }
        };

        await container.CreateItemAsync(
            item,
            new PartitionKey(item.LastName));

        #region FeedResponse

        using var iterator = container.GetItemLinqQueryable<Family>()
            .Where(b => b.Id == item.Id)
            .ToFeedIterator();
        var feedResponse = await iterator.ReadNextAsync();
        await Verify(feedResponse);

        #endregion
    }
}