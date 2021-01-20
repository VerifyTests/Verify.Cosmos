# <img src="/src/icon.png" height="30px"> Verify.Cosmos

[![Build status](https://ci.appveyor.com/api/projects/status/wwrri8srggv1h56j/branch/master?svg=true)](https://ci.appveyor.com/project/SimonCropp/Verify-Cosmos)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.Cosmos.svg)](https://www.nuget.org/packages/Verify.Cosmos/)

Adds [Verify](https://github.com/VerifyTests/Verify) support to verify [Azure CosmosDB](https://docs.microsoft.com/en-us/azure/cosmos-db/).




<!-- toc -->
## Contents

  * [Usage](#usage)
    * [ItemResponse](#itemresponse)<!-- endToc -->


## NuGet package

https://nuget.org/packages/Verify.Cosmos/


## Usage

Before any tests have run call:

```
VerifyCosmos.Enable();
```


### ItemResponse

A `ItemResponse` can be verified:

<!-- snippet: ItemResponse -->
<a id='snippet-itemresponse'></a>
```cs
var response = await container.CreateItemAsync(
    item,
    new PartitionKey(item.LastName));
await Verifier.Verify(response);
```
<sup><a href='/src/Tests/Tests.cs#L51-L56' title='Snippet source file'>snippet source</a> | <a href='#snippet-itemresponse' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

Resulting in: 

<!-- snippet: Tests.ItemResponse.verified.txt -->
<a id='snippet-Tests.ItemResponse.verified.txt'></a>
```txt
{
  Headers: {},
  Resource: {
    id: Guid_1,
    LastName: Andersen,
    Address: {
      State: WA,
      County: King,
      City: Seattle
    }
  },
  StatusCode: Created,
  Diagnostics: {},
  RequestCharge: 7.43,
  ActivityId: Guid_2
}
```
<sup><a href='/src/Tests/Tests.ItemResponse.verified.txt#L1-L16' title='Snippet source file'>snippet source</a> | <a href='#snippet-Tests.ItemResponse.verified.txt' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Icon

[Approval](https://thenounproject.com/term/approval/1759519/) designed by [Mike Zuidgeest](https://thenounproject.com/zuidgeest/) from [The Noun Project](https://thenounproject.com/).
