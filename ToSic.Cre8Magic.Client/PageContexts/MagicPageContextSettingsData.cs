using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Debug;

namespace ToSic.Cre8magic.PageContexts;

public record MagicPageContextSettingsData: SettingsWithInherit, IHasDebugSettings, ICanClone<MagicPageContextSettingsData>
{
    public MagicPageContextSettingsData() { }

    [PrivateApi]
    public MagicPageContextSettingsData(MagicPageContextSettingsData? priority, MagicPageContextSettingsData? fallback = default)
        : base(priority, fallback)
    {
        UseBodyTag = priority?.UseBodyTag ?? fallback?.UseBodyTag;
        Debug = priority?.Debug ?? fallback?.Debug;
    }

    [PrivateApi]
    public MagicPageContextSettingsData CloneUnder(MagicPageContextSettingsData? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);
    

    public bool? UseBodyTag { get; init; }
    internal bool UseBodyTagSafe => UseBodyTag ?? false;

    public MagicDebugSettings? Debug { get; init; }

    internal static Defaults<MagicPageContextSettingsData> Defaults = new(new()
    {
        UseBodyTag = false,
    });
}