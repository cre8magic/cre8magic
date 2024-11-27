using System.Text.Json.Serialization;
using Oqtane.UI;
using ToSic.Cre8magic.Settings.Internal.Debug;
using ToSic.Cre8magic.Settings.Internal.Docs;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// Internal intermediate base class containing all kinds of settings which
/// all settings share.
/// 
/// </summary>
public abstract record MagicSettingsBase: SettingsWithInherit, ISettingsForCodeUse, IDebugSettings
{
    #region Constructor & Cloning

    [PrivateApi]
    protected MagicSettingsBase() { }

    [PrivateApi]
    protected MagicSettingsBase(MagicSettingsBase? priority, MagicSettingsBase? fallback = default)
        : base(priority, fallback)
    {
        if (fallback == null)
            return;

        PartName = priority?.PartName ?? fallback.PartName;
        SettingsName = priority?.SettingsName ?? fallback.SettingsName;
        DesignName = priority?.DesignName ?? fallback.DesignName;

        // Page State
        _pageState = priority?.PageState ?? fallback.PageState;

        // Debug settings
        ((IDebugSettings)this).Catalog = ((IDebugSettings?)priority)?.Catalog ?? ((IDebugSettings?)fallback)?.Catalog;
        ((IDebugSettings)this).DebugThis = ((IDebugSettings?)priority)?.DebugThis ?? ((IDebugSettings?)fallback)?.DebugThis;
    }

    #endregion


    #region Settings for Code

    /// <summary>
    /// The PageState which is needed for doing everything.
    ///
    /// It can be provided in the settings, or it must be provided in the theme using <see cref="IMagicHat.UsePageState"/>.
    /// </summary>
    [JsonIgnore]
    public virtual PageState? PageState
    {
        get => _pageState;
        init => _pageState = value;
    }
    private readonly PageState? _pageState;


    /// <inheritdoc/>
    [JsonIgnore]
    public string? PartName { get; init; }

    /// <inheritdoc/>
    [JsonIgnore]
    public string? SettingsName { get; init; }

    /// <inheritdoc/>
    [JsonIgnore]
    public string? DesignName { get; init; }

    #endregion

    #region Debug Settings

    [JsonIgnore]
    MagicSettingsCatalog? IDebugSettings.Catalog { get; set; }

    [JsonIgnore]
    bool? IDebugSettings.DebugThis { get; set; }

    #endregion
}