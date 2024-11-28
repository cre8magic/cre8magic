using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Breadcrumbs;

/// <summary>
/// Language Design Settings
/// </summary>
public record MagicBreadcrumbDesignSettings : SettingsWithInherit, ICanClone<MagicBreadcrumbDesignSettings>
{
    public MagicBreadcrumbDesignSettings() { }

    private MagicBreadcrumbDesignSettings(MagicBreadcrumbDesignSettings? priority, MagicBreadcrumbDesignSettings? fallback = default)
        : base(priority, fallback)
    {
        Parts = MergeHelper.CloneMergeDictionaries(priority?.Parts, fallback?.Parts);
    }

    MagicBreadcrumbDesignSettings ICanClone<MagicBreadcrumbDesignSettings>.CloneUnder(MagicBreadcrumbDesignSettings? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    /// <summary>
    /// Custom, named settings for classes, values etc. as you need them in your code.
    /// For things such as `ul` or `li` or `a` tags.
    /// </summary>
    //[JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicBreadcrumbDesignSettingsPart>))]
    public Dictionary<string, MagicBreadcrumbDesignSettingsPart> Parts { get; init; } = new();


    internal static Defaults<MagicBreadcrumbDesignSettings> DesignDefaults = new()
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