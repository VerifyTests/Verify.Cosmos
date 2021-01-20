namespace VerifyTests
{
    public static class VerifyCosmos
    {
        public static void Enable()
        {
            VerifierSettings.ModifySerialization(settings =>
            {
 //               settings.IgnoreMembersWithType<IBuilder>();
                settings.AddExtraSettings(serializerSettings =>
                {
                    //var converters = serializerSettings.Converters;
                });
            });
        }
    }
}