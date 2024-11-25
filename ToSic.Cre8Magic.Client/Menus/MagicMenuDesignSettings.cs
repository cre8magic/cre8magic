using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Json;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Menus;

/// <summary>
/// Menu Design Settings
/// </summary>
public record MagicMenuDesignSettings : SettingsWithInherit, ICanClone<MagicMenuDesignSettings>
{
    public MagicMenuDesignSettings() { }

    [PrivateApi]
    public MagicMenuDesignSettings(MagicMenuDesignSettings? priority, MagicMenuDesignSettings? fallback = default)
        : base(priority, fallback)
    {
        Parts = MergeHelper.CloneMergeDictionaries(priority?.Parts, fallback?.Parts);
    }

    [PrivateApi]
    public MagicMenuDesignSettings CloneUnder(MagicMenuDesignSettings? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    /// <summary>
    /// Custom, named settings for classes, values etc. as you need them in your code.
    /// For things such as `ul` or `li` or `a` tags.
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicMenuDesignSettingsPart>))]
    public Dictionary<string, MagicMenuDesignSettingsPart> Parts { get; init; } = new();

}