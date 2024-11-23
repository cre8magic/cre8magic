namespace ToSic.Cre8magic.Settings.Internal.Debug;

public static class IDebugSettingsExtensions
{
    public static void UseCatalog(this IDebugSettings settings, MagicSettingsCatalog catalog)
    {
        settings.Catalog = catalog;
    }
    public static T UseCatalog<T>(this T settings, MagicSettingsCatalog catalog) where T : IDebugSettings
    {
        settings.Catalog = catalog;
        return settings;
    }
}