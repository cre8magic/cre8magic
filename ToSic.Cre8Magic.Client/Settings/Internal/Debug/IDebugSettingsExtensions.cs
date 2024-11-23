namespace ToSic.Cre8magic.Settings.Internal.Debug;

public static class IDebugSettingsExtensions
{
    public static T UseCatalog<T>(this T settings, MagicSettingsCatalog catalog) where T : IDebugSettings
    {
        settings.Catalog = catalog;
        return settings;
    }

    public static T UseLanguageSettings<T>(this T settings, MagicLanguageSettingsData languageSettings) where T : IDebugSettings
    {
        settings.Catalog = (settings.Catalog ?? new MagicSettingsCatalog()) with
        {
            Languages = new() { { "default", languageSettings } }
        };
        return settings;
    }
}