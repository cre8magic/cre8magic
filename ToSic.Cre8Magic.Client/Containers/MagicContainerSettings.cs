using System.Text.Json.Serialization;
using Oqtane.Models;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Containers;

public record MagicContainerSettings: MagicSettings, ICanClone<MagicContainerSettings>, IWith<Module?>
{
    [PrivateApi]
    public MagicContainerSettings() { }

    private MagicContainerSettings(MagicContainerSettings? priority, MagicContainerSettings? fallback = default)
        : base(priority, fallback)
    {
        Blueprint = priority?.Blueprint ?? fallback?.Blueprint;
        ModuleState = priority?.ModuleState ?? fallback?.ModuleState;
    }

    MagicContainerSettings ICanClone<MagicContainerSettings>.CloneUnder(MagicContainerSettings? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    [JsonIgnore]
    public MagicContainerBlueprint? Blueprint { get; init; }

    public Module? ModuleState { get; init; }

    Module? IWith<Module?>.WithData { get => ModuleState; init => ModuleState = value; }

    #region Stabilized

    [PrivateApi]
    public Stabilized GetStable() => (_stabilized ??= new(new(this))).Value;
    private IgnoreEquals<Stabilized>? _stabilized;

    /// <summary>
    /// Experimental 2025-03-25 2dm
    /// Purpose is to allow all settings to be nullable, but have a robust reader that will always return a value,
    /// so that the code using the values doesn't need to check for nulls.
    /// </summary>
    [PrivateApi]
    public new record Stabilized(MagicContainerSettings LanguageSettings) : MagicSettings.Stabilized(LanguageSettings)
    {
        public MagicContainerBlueprint? Blueprint => LanguageSettings.Blueprint;
        public Module ModuleState => LanguageSettings.ModuleState ?? new();
    }

    #endregion

}