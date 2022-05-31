using Microsoft.Azure.Cosmos;

namespace VerifyTests;

public static class VerifyCosmos
{
    public static void Enable()
    {
        VerifierSettings.IgnoreMembers("ETag");
        VerifierSettings.IgnoreMember<Database>(x => x.Client);
        VerifierSettings.IgnoreMembersWithType<CosmosDiagnostics>();
        VerifierSettings.IgnoreMembersWithType<IndexingPolicy>();
        VerifierSettings.IgnoreMembersWithType<ContainerProperties>();
        VerifierSettings.IgnoreMembersWithType<DatabaseProperties>();
        VerifierSettings.AddExtraSettings(serializerSettings =>
        {
            var converters = serializerSettings.Converters;
            converters.Add(new HeadersConverter());
            converters.Add(new FeedResponseConverter());
            converters.Add(new ResponseConverter());
        });
    }
}