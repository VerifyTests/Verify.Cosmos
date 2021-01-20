using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using VerifyXunit;
using Xunit;

[UsesVerify]
public class Tests
{
    [Fact]
    public async Task Simple()
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
            Id = "Andersen.1",
            LastName = "Andersen",
            Parents = new Parent[]
            {
                new(){ FirstName = "Thomas" },
                new(){ FirstName = "Mary Kay" }
            },
            Children = new Child[]
            {
                new()
                {
                    FirstName = "Henriette Thaulow",
                    Gender = "female",
                    Grade = 5,
                    Pets = new Pet[]
                    {
                        new(){ GivenName = "Fluffy" }
                    }
                }
            },
            Address = new Address
            {
                State = "WA",
                County = "King",
                City = "Seattle"
            },
            IsRegistered = false
        };

        // Read the item to see if it exists.
        var response = await container.ReadItemAsync<Family>(family.Id, new PartitionKey(family.LastName));

        await Verifier.Verify(response);
    }
}