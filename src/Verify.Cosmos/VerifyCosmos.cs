using Microsoft.Azure.Cosmos;

namespace VerifyTests
{
    public static class VerifyCosmos
    {
        public static void Enable()
        {
            VerifierSettings.ModifySerialization(settings =>
            {
                settings.IgnoreMembers("ETag");
                settings.IgnoreMember<Database>(x => x.Client);
                settings.IgnoreMembersWithType<IndexingPolicy>();
                settings.IgnoreMember<ContainerResponse>(x => x.Resource);
                settings.IgnoreMembersWithType<DatabaseProperties>();
                settings.AddExtraSettings(serializerSettings =>
                {
                    var converters = serializerSettings.Converters;
                    converters.Add(new HeadersConverter());
                    converters.Add(new FeedResponseConverter());
                });
            });
        }
    }
}