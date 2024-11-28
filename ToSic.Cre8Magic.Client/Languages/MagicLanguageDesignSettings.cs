using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Json;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Languages;

/// <summary>
/// Language Design Settings
/// </summary>
public record MagicLanguageDesignSettings : SettingsWithInherit, ICanClone<MagicLanguageDesignSettings>
{
    [PrivateApi]
    public MagicLanguageDesignSettings() { }

    private MagicLanguageDesignSettings(MagicLanguageDesignSettings? priority, MagicLanguageDesignSettings? fallback = default)
        : base(priority, fallback)
    {
        Parts = MergeHelper.CloneMergeDictionaries(priority?.Parts, fallback?.Parts);
    }

    MagicLanguageDesignSettings ICanClone<MagicLanguageDesignSettings>.CloneUnder(MagicLanguageDesignSettings? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    /// <summary>
    /// Custom, named settings for classes, values etc. as you need them in your code.
    /// For things such as `ul` or `li` or `a` tags.
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicDesignSettingsPart>))]
    public Dictionary<string, MagicDesignSettingsPart> Parts { get; init; } = new();


    internal static Defaults<MagicLanguageDesignSettings> DesignDefaults = new()
    {
        Fallback = new()
        {
            Parts = new()
            {
                { "li", new() { IsActive = new() { On = "active" } } }
            },
        },
        Foundation = new()
    };
}