using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings.Internal.Debug;
using ToSic.Cre8magic.Settings.Internal.Docs;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// Internal intermediate base class containing all kinds of settings which
/// all settings share.
/// 
/// </summary>
public abstract record MagicSettingsBase: SettingsWithInherit, ISettingsForCodeUse, IDebugSettings //, ICanClone<MagicSettingsBase>
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
        ((IDebugSettings)this).Catalog = ((IDebugSettings?)priority)?.Catalog ?? ((IDebugSettings?)fallback)?.Catalog;
        
        ((IDebugSettings)this).DebugThis = ((IDebugSettings?)priority)?.DebugThis ?? ((IDebugSettings?)fallback)?.DebugThis;


    }

    //[PrivateApi]
    //public MagicSettingsBase CloneUnder(MagicSettingsBase? priority, bool forceCopy = false) =>
    //    priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    #endregion


    #region Settings for Code

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