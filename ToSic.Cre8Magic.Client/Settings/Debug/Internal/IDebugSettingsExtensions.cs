namespace ToSic.Cre8magic.Settings.Debug.Internal;

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
}