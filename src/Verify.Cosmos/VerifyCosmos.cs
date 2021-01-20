namespace VerifyTests
{
    public static class VerifyCosmos
    {
        public static void Enable()
        {
            VerifierSettings.ModifySerialization(settings =>
            {
                settings.IgnoreMembers("ETag");
                settings.AddExtraSettings(serializerSettings =>
                {
                    var converters = serializerSettings.Converters;
                    converters.Add(new HeadersConverter());
                });
            });
        }
    }
}