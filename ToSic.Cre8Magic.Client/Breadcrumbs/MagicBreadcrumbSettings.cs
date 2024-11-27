using System.Text.Json.Serialization;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings.Internal.Debug;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal.Docs;

namespace ToSic.Cre8magic.Breadcrumbs;

public record MagicBreadcrumbSettings: MagicBreadcrumbSettingsData, ISettingsForCodeUse, IDebugSettings
{
    public MagicBreadcrumbSettings() { }

    /// <summary>
    /// Constructor to re-hydrate from object of base class.
    /// </summary>
    [PrivateApi]
    internal MagicBreadcrumbSettings(MagicBreadcrumbSettingsData ancestor, MagicBreadcrumbSettings? original) : base(ancestor)
    {
        if (original == null)
            return;

        Designer = original.Designer;
        PartName = original.PartName;
        SettingsName = original.SettingsName;
        DesignName = original.DesignName;
        DesignSettings = original.DesignSettings;

        ((IDebugSettings)this).Catalog = ((IDebugSettings)original).Catalog;
    }

    #region Settings for Code

    /// <inheritdoc/>
    public string? PartName { get; init; }

    /// <inheritdoc/>
    public string? SettingsName { get; init; }

    /// <inheritdoc/>
    public string? DesignName { get; init; }

    #endregion

    [JsonIgnore] // Marked as JsonIgnore to ensure we know that ATM there is no JSON support expected of this property
    public IMagicPageDesigner? Designer { get; init; }

    public MagicBreadcrumbDesignSettings? DesignSettings { get; init; }


    MagicSettingsCatalog? IDebugSettings.Catalog { get; set; }
    bool? IDebugSettings.DebugThis { get; set; }
}