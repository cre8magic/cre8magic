using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Json;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Breadcrumbs;

/// <summary>
/// Language Design Settings
/// </summary>
public record MagicBreadcrumbBlueprint : SettingsWithInherit, ICanClone<MagicBreadcrumbBlueprint>
{
    [PrivateApi]
    public MagicBreadcrumbBlueprint() { }

    private MagicBreadcrumbBlueprint(MagicBreadcrumbBlueprint? priority, MagicBreadcrumbBlueprint? fallback = default)
        : base(priority, fallback)
    {
        Parts = MergeHelper.CloneMergeDictionaries(priority?.Parts, fallback?.Parts);
    }

    MagicBreadcrumbBlueprint ICanClone<MagicBreadcrumbBlueprint>.CloneUnder(MagicBreadcrumbBlueprint? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    /// <summary>
    /// Custom, named settings for classes, values etc. as you need them in your code.
    /// For things such as `ul` or `li` or `a` tags.
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicBlueprintPart>))]
    public Dictionary<string, MagicBlueprintPart> Parts { get; init; } = new();


    internal static Defaults<MagicBreadcrumbBlueprint> DesignDefaults = new()
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