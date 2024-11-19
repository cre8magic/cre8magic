namespace ToSic.Cre8magic.Services.Internal;

public static class MagicSettingsDebugExtensions
{
    public static TDebug NoCachingForDebug<TDebug>(
        this IMagicSettingsService settings,
        Func<IMagicSettingsService, TDebug> func
    ) => settings.BypassCacheInternal(func);
}