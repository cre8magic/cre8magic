using System.Text.Json.Serialization;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings.Internal.Docs;

namespace ToSic.Cre8magic.Menus;

public record MagicMenuSettings: MagicMenuSettingsData, ISettingsForCodeUse
{
    /// <summary>
    /// Default constructor, so this record can be created anywhere.
    /// </summary>
    public MagicMenuSettings() { }
    
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
        SettingsName = original.SettingsName;
        DesignName = original.DesignName;
        PartName = original.PartName;
        Pages = original.Pages;
    }

    /// <inheritdoc />
    public string? PartName { get; init; }

    /// <inheritdoc />
    public string? SettingsName { get; init; }

    public string? DesignName { get; init; }

    [JsonIgnore]    // Not meant for JSON at all...
    public IMagicPageDesigner? Designer { get; init; }

    public MagicMenuDesignSettings? DesignSettings { get; init; } = new();

    /// <summary>
    /// List of pages to respect when creating the breadcrumb.
    /// Default is `null` - so it will just take all the pages.
    ///
    /// TODO: NAMING
    /// </summary>
    [JsonIgnore]    // Not meant for JSON at all...
    public IEnumerable<IMagicPage>? Pages { get; init; } = null;


}