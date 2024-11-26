using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal.Debug;
using ToSic.Cre8magic.Settings.Internal.Docs;

namespace ToSic.Cre8magic.PageContexts;

public record MagicPageContextSettings: MagicPageContextSettingsData, ISettingsForCodeUse, IDebugSettings
{
    public MagicPageContextSettings() { }

    /// <summary>
    /// Constructor to re-hydrate from object of base class.
    /// </summary>
    [PrivateApi]
    internal MagicPageContextSettings(MagicPageContextSettingsData ancestor, MagicPageContextSettings? original) : base(ancestor)
    {
        if (original == null)
            return;

        PartName = original.PartName;
        SettingsName = original.SettingsName;
        DesignName = original.DesignName;
        //DesignSettings = original.DesignSettings;

        ((IDebugSettings)this).Catalog = ((IDebugSettings)original).Catalog;
    }

    //public bool? UseBodyTag { get; init; }

    public string? Classes { get; init; }

    public string? TagId { get; init; }

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