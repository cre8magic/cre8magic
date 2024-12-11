namespace ToSic.Cre8magic.Settings.Internal.Debug;

public static class IDebugSettingsExtensions
{
    public static T Debug<T>(this T settings) where T : IDebugSettings
    {
        settings.DebugThis = true;
        return settings;
    }

    public static T UseSpellsBook<T>(this T settings, MagicSpellsBook book) where T : IDebugSettings
    {
        settings.Book = book;
        return settings;
    }

    public static T UseLanguageSettings<T>(this T settings, MagicLanguageSpell languageSpell) where T : IDebugSettings
    {
        settings.Book = (settings.Book ?? new MagicSpellsBook()) with
        {
            Languages = new() { { "default", languageSpell } }
        };
        return settings;
    }
}