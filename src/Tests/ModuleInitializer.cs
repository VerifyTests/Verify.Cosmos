public static class ModuleInitializer
{
    #region enable

    [ModuleInitializer]
    public static void Init()
    {
        VerifyCosmos.Enable();

        #endregion

        VerifyDiffPlex.Initialize();
    }
}