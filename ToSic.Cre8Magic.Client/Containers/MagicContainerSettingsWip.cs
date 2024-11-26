using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal.Debug;
using ToSic.Cre8magic.Settings.Internal.Docs;

namespace ToSic.Cre8magic.Containers;

public record MagicContainerSettingsWip: MagicContainerSettings, ISettingsForCodeUse, IDebugSettings
{
    public MagicContainerSettingsWip() { }

    /// <summary>
    /// Constructor to re-hydrate from object of base class.
    /// </summary>
    [PrivateApi]
    internal MagicContainerSettingsWip(MagicContainerSettings ancestor, MagicContainerSettingsWip? original) : base(ancestor)
    {
        if (original == null)
            return;

        PartName = original.PartName;
        SettingsName = original.SettingsName;
        DesignName = original.DesignName;

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

    #region Debug Settings

    MagicSettingsCatalog? IDebugSettings.Catalog { get; set; }

    bool IDebugSettings.DebugThis { get; set; }

    #endregion

}