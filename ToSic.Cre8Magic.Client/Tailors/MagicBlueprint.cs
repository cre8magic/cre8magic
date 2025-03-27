using System.Text.Json.Serialization;
using ToSic.Cre8magic.Internal.Json;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Tailors;

/// <summary>
/// Menu Design Settings
/// </summary>
public record MagicBlueprint : MagicInheritsBase, ICanClone<MagicBlueprint>
{
    #region Constructor and Clone

    [PrivateApi]
    public MagicBlueprint() { }

    internal MagicBlueprint(MagicBlueprint? priority, MagicBlueprint? fallback = default)
        : base(priority, fallback)
    {
        Parts = MergeHelper.CloneMergeDictionaries(priority?.Parts, fallback?.Parts);
    }

    MagicBlueprint ICanClone<MagicBlueprint>.CloneUnder(MagicBlueprint? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    #endregion

    /// <summary>
    /// Custom, named settings for classes, values etc. as you need them in your code.
    /// For things such as `ul` or `li` or `a` tags.
    /// </summary>
    [JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicBlueprintPart>))]
    public Dictionary<string, MagicBlueprintPart>? Parts { get; init; }

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
    public new record Stabilized(MagicBlueprint BlueprintSettings) : MagicInheritsBase.Stabilized(BlueprintSettings)
    {
        public Dictionary<string, MagicBlueprintPart> Parts =>
            BlueprintSettings.Parts ?? new(StringComparer.OrdinalIgnoreCase);
    }

    #endregion

}