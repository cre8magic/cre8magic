namespace ToSic.Cre8magic.Settings.Internal.Debug;

public static class IDebugSettingsExtensions
{
    public static T Debug<T>(this T settings) where T : IDebugSettings
    {
        settings.DebugThis = true;
        return settings;
    }

    public static T UseBook<T>(this T settings, MagicBook book) where T : IDebugSettings
    {
        settings.Book = book;
        return settings;
    }

    // TODO: this was probably an old experiment to inject alternate defaults
    // should probably be removed again
    public static T UseLanguageSettings<T>(this T settings, MagicLanguageSettings languageSettings) where T : IDebugSettings
    {
        settings.Book = (settings.Book ?? new MagicBook()) with
        {
            Languages = new() { { "default", languageSettings } }
        };
        return settings;
    }
}