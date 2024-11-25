using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings.Internal.Docs;

namespace ToSic.Cre8magic.Breadcrumbs;

public record MagicBreadcrumbSettingsWip: MagicBreadcrumbSettingsData, ISettingsForCodeUse
{
    public MagicBreadcrumbSettingsWip() { }

    /// <summary>
    /// Constructor to re-hydrate from object of base class.
    /// </summary>
    [PrivateApi]
    internal MagicBreadcrumbSettingsWip(MagicBreadcrumbSettingsData ancestor, MagicBreadcrumbSettingsWip? original) : base(ancestor)
    {
        if (original == null)
            return;

        PartName = original.PartName;
        SettingsName = original.SettingsName;
        DesignName = original.DesignName;
        DesignSettings = original.DesignSettings;
    }

    #region Settings for Code

    /// <inheritdoc/>
    public string? PartName { get; init; }

    /// <inheritdoc/>
    public string? SettingsName { get; init; }

    /// <inheritdoc/>
    public string? DesignName { get; init; }

    #endregion

    // todo: name, maybe not on interface
    [JsonIgnore] // TODO: MOVE TO DESIGN SETTINGS after renaming this to ...Data
    public MagicBreadcrumbDesignSettings? DesignSettings { get; init; }

}