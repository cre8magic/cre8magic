using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Json;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Themes.Settings;

/// <summary>
/// Constants and helpers related to creating Css and Css Classes.
///
/// If you change these, you must also update the SCSS files. 
/// </summary>
public partial record MagicThemeDesignSettings: SettingsWithInherit, ICanClone<MagicThemeDesignSettings>
{
    public MagicThemeDesignSettings() { }

    public MagicThemeDesignSettings(MagicThemeDesignSettings? priority, MagicThemeDesignSettings? fallback = default)
        : base(priority, fallback)
    {
        MagicContext = priority?.MagicContext ?? fallback?.MagicContext ?? [];
        PageIsHome = priority?.PageIsHome ?? fallback?.PageIsHome;
        PaneIsEmpty = priority?.PaneIsEmpty ?? fallback?.PaneIsEmpty;
        MagicContextTagId = priority?.MagicContextTagId ?? fallback?.MagicContextTagId;

        Parts = MergeHelper.CloneMergeDictionaries(priority?.Parts, fallback?.Parts);
    }

    public MagicThemeDesignSettings CloneUnder(MagicThemeDesignSettings? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    public string[] MagicContext { get; init; } = [];

    public PairOnOff? PageIsHome { get; init; }

    public PairOnOff? PaneIsEmpty { get; init; }

    public string? MagicContextTagId { get; init; }

    /// <summary>
    /// Custom values / classes as you need them in your code
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicDesignSettings>))]
    public Dictionary<string, MagicDesignSettings> Parts { get; init; } = new();

}