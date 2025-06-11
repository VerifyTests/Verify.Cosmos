# <img src="/src/icon.png" height="30px"> Verify.Cosmos

[![Discussions](https://img.shields.io/badge/Verify-Discussions-yellow?svg=true&label=)](https://github.com/orgs/VerifyTests/discussions)
[![Build status](https://ci.appveyor.com/api/projects/status/89flq4nfrcmnykd0?svg=true)](https://ci.appveyor.com/project/SimonCropp/Verify-Cosmos)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.Cosmos.svg)](https://www.nuget.org/packages/Verify.Cosmos/)

Adds [Verify](https://github.com/VerifyTests/Verify) support to verify [Azure CosmosDB](https://docs.microsoft.com/en-us/azure/cosmos-db/).

**See [Milestones](../../milestones?state=closed) for release notes.**


## Sponsors

include: zzz


## NuGet

 * https://nuget.org/packages/Verify.Cosmos


## Usage

<!-- snippet: enable -->
<a id='snippet-enable'></a>
```cs
[ModuleInitializer]
public static void Init() =>
    VerifyCosmos.Initialize();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L3-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-enable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### ItemResponse

A `ItemResponse` can be verified:

<!-- snippet: ItemResponse -->
<a id='snippet-ItemResponse'></a>
```cs
var response = await container.CreateItemAsync(
    item,
    new PartitionKey(item.LastName));
await Verify(response);
```
<sup><a href='/src/Tests/Tests.cs#L44-L51' title='Snippet source file'>snippet source</a> | <a href='#snippet-ItemResponse' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Resulting in:

<!-- snippet: Tests.ItemResponse.verified.txt -->
<a id='snippet-Tests.ItemResponse.verified.txt'></a>
```txt
{
  RequestCharge: 7.4,
  Headers: {},
  StatusCode: Created,
  Resource: {
    Id: Guid_1,
    LastName: Andersen,
    Address: {
      State: WA,
      County: King,
      City: Seattle
    }
  }
}
```
<sup><a href='/src/Tests/Tests.ItemResponse.verified.txt#L1-L14' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.ItemResponse.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### FeedResponse

A `FeedResponse` can be verified:

<!-- snippet: FeedResponse -->
<a id='snippet-FeedResponse'></a>
```cs
using var iterator = container.GetItemLinqQueryable<Family>()
    .Where(b => b.Id == item.Id)
    .ToFeedIterator();
var feedResponse = await iterator.ReadNextAsync();
await Verify(feedResponse);
```
<sup><a href='/src/Tests/Tests.cs#L77-L85' title='Snippet source file'>snippet source</a> | <a href='#snippet-FeedResponse' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Resulting in:

<!-- snippet: Tests.FeedResponse.verified.txt -->
<a id='snippet-Tests.FeedResponse.verified.txt'></a>
```txt
{
  RequestCharge: 2.8,
  Count: 1,
  Headers: {},
  StatusCode: OK,
  Resource: [
    {
      Id: Guid_1,
      LastName: Andersen,
      Address: {
        State: WA,
        County: King,
        City: Seattle
      }
    }
  ]
}
```
<sup><a href='/src/Tests/Tests.FeedResponse.verified.txt#L1-L17' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.FeedResponse.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Icon

[Approval](https://thenounproject.com/term/approval/1759519/) designed by [Mike Zuidgeest](https://thenounproject.com/zuidgeest/) from [The Noun Project](https://thenounproject.com/).
