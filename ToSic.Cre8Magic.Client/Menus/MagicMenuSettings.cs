using System.Text.Json.Serialization;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Docs;

namespace ToSic.Cre8magic.Menus;

public record MagicMenuSettings: MagicMenuSettingsData, ISettingsWithNames, ICanClone<MagicMenuSettings>
{
    /// <summary>
    /// Default constructor, so this record can be created anywhere.
    /// </summary>
    public MagicMenuSettings() { }

    internal MagicMenuSettings(MagicMenuSettings? priority, MagicMenuSettings? fallback = default)
        : base(priority, fallback)
    {
        Designer = priority?.Designer ?? fallback?.Designer;
        PartName = priority?.PartName ?? fallback?.PartName;
        Pages = priority?.Pages ?? fallback?.Pages;

        // TODO: #NamedSettings
        DesignSettings = priority?.DesignSettings ?? fallback?.DesignSettings;

    }

    /// <summary>
    /// Constructor to re-hydrate from object of base class.
    /// </summary>
    /// <param name="ancestor"></param>
    /// <param name="original"></param>
    internal MagicMenuSettings(MagicMenuSettingsData ancestor, MagicMenuSettings? original) : base(ancestor)
    {
        if (original == null)
            return;
        Designer = original.Designer;
        DesignSettings = original.DesignSettings;
        PartName = original.PartName;
        Pages = original.Pages;
    }

    MagicMenuSettings ICanClone<MagicMenuSettings>.CloneUnder(MagicMenuSettings? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);



    /// <inheritdoc />
    public string? PartName { get; init; }

    [JsonIgnore]    // Not meant for JSON at all...
    public IMagicPageDesigner? Designer { get; init; }

    // todo: name, maybe not on interface
    [JsonIgnore] // can't be provided in JSON, but could in code
    public Dictionary<string, MagicMenuDesignSettings>? DesignSettings { get; init; }

    /// <summary>
    /// List of pages to respect when creating the breadcrumb.
    /// Default is `null` - so it will just take all the pages.
    ///
    /// TODO: NAMING
    /// </summary>
    [JsonIgnore]    // Not meant for JSON at all...
    public IEnumerable<IMagicPage>? Pages { get; init; } = null;


}