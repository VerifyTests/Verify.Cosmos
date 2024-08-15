namespace VerifyTests;

public static class VerifyCosmos
{
    internal static double RoundRequestCharge(dynamic o)
    {
        var requestCharge = (double) o.RequestCharge;
        return Math.Round(requestCharge, 1);
    }

    public static bool Initialized { get; private set; }

    public static void Initialize()
    {
        if (Initialized)
        {
            throw new("Already Initialized");
        }

        Initialized = true;

        InnerVerifier.ThrowIfVerifyHasBeenRun();
        VerifierSettings.IgnoreMembers("ETag");
        VerifierSettings.IgnoreMember<Database>(_ => _.Client);
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