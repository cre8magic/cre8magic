using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Debug;
using ToSic.Cre8magic.Settings.Internal.Json;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.PageContexts;

public record MagicPageContextSettingsData: SettingsWithInherit, IHasDebugSettings, ICanClone<MagicPageContextSettingsData>
{
    public MagicPageContextSettingsData() { }

    [PrivateApi]
    public MagicPageContextSettingsData(MagicPageContextSettingsData? priority, MagicPageContextSettingsData? fallback = default)
        : base(priority, fallback)
    {
        UseBodyTag = priority?.UseBodyTag ?? fallback?.UseBodyTag;

        ClassList = priority?.ClassList ?? fallback?.ClassList ?? [];
        PageIsHome = priority?.PageIsHome ?? fallback?.PageIsHome;
        PaneIsEmpty = priority?.PaneIsEmpty ?? fallback?.PaneIsEmpty;
        MagicContextTagId = priority?.MagicContextTagId ?? fallback?.MagicContextTagId;

        Parts = MergeHelper.CloneMergeDictionaries(priority?.Parts, fallback?.Parts);


        Debug = priority?.Debug ?? fallback?.Debug;
    }

    [PrivateApi]
    public MagicPageContextSettingsData CloneUnder(MagicPageContextSettingsData? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);
    

    public bool? UseBodyTag { get; init; }

    internal bool UseBodyTagSafe => UseBodyTag ?? false;

    public string[]? ClassList { get; init; }

    public MagicSettingOnOff? PageIsHome { get; init; }

    public MagicSettingOnOff? PaneIsEmpty { get; init; }

    public string? MagicContextTagId { get; init; }




    /// <summary>
    /// Custom values / classes as you need them in your code
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicDesignSettingsPart>))]
    public Dictionary<string, MagicDesignSettingsPart> Parts { get; init; } = new();


    public MagicDebugSettings? Debug { get; init; }

    internal static Defaults<MagicPageContextSettingsData> Defaults = new();
}