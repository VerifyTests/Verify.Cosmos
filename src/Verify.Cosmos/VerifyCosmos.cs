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
                settings.IgnoreMember<Database>(x=>x.Client);
                settings.IgnoreMembersWithType<DatabaseProperties>();
                //settings.IgnoreMembersWithType<CosmosClientOptions>();
                settings.AddExtraSettings(serializerSettings =>
                {
                    var converters = serializerSettings.Converters;
                    converters.Add(new HeadersConverter());
                });
            });
        }
    }
}