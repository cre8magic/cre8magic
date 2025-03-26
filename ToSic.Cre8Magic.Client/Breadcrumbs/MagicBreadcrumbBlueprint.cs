using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Tailors;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Breadcrumbs;

/// <summary>
/// Language Design Settings
/// </summary>
public record MagicBreadcrumbBlueprint : MagicBlueprint, ICanClone<MagicBreadcrumbBlueprint>
{
    [PrivateApi]
    public MagicBreadcrumbBlueprint() { }

    private MagicBreadcrumbBlueprint(MagicBreadcrumbBlueprint? priority, MagicBreadcrumbBlueprint? fallback = default)
        : base(priority, fallback)
    { }

    MagicBreadcrumbBlueprint ICanClone<MagicBreadcrumbBlueprint>.CloneUnder(MagicBreadcrumbBlueprint? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);


    #region Stabilized

    [PrivateApi]
    public new Stabilized GetStable() => (_stabilized ??= new(new(this))).Value;
    private IgnoreEquals<Stabilized>? _stabilized;

    /// <summary>
    /// Experimental 2025-03-25 2dm
    /// Purpose is to allow all settings to be nullable, but have a robust reader that will always return a value,
    /// so that the code using the values doesn't need to check for nulls.
    /// </summary>
    [PrivateApi]
    public new record Stabilized(MagicBreadcrumbBlueprint LanguageBlueprint) : MagicBlueprint.Stabilized(LanguageBlueprint)
    {
        [field: AllowNull, MaybeNull]
        public new Dictionary<string, MagicBlueprintPart> Parts => field
            ??= LanguageBlueprint.Parts ?? new(StringComparer.OrdinalIgnoreCase)
            {
                { "li", new() { IsActive = new() { On = "active" } } }
            };
    }

    #endregion
}