using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Breadcrumbs;

/// <summary>
/// Breadcrumb settings - either provided in code, or generated from JSON.
/// </summary>
/// <remarks>
/// NOTE that as of v0.2 the JSON variant is not in use.
/// </remarks>
public record MagicBreadcrumbSettings : MagicSettings, IMagicPageSetSettings, ICanClone<MagicBreadcrumbSettings>, IWith<IMagicPageTailor?>, IWith<MagicBreadcrumbBlueprint?>
{
    /// <summary>
    /// Note: needs standalone; non-primary constructor, so that the private clone constructor can call the base constructor.
    /// </summary>
    [PrivateApi]
    public MagicBreadcrumbSettings() { }

    /// <summary>
    /// Cloning constructor
    /// </summary>
    private MagicBreadcrumbSettings(MagicBreadcrumbSettings? priority, MagicBreadcrumbSettings? fallback = default): base(priority, fallback)
    {
        WithActive = priority?.WithActive ?? fallback?.WithActive;
        WithHome = priority?.WithHome ?? fallback?.WithHome;
        MaxDepth = priority?.MaxDepth ?? fallback?.MaxDepth;
        Reverse = priority?.Reverse ?? fallback?.Reverse;
        Pages = priority?.Pages ?? fallback?.Pages;
        Active = priority?.Active ?? fallback?.Active;
        Id = priority?.Id ?? fallback?.Id;
        MenuId = priority?.MenuId ?? fallback?.MenuId;
        Show = priority?.Show ?? fallback?.Show;
        ActiveId = priority?.ActiveId ?? fallback?.ActiveId;
        Variant = priority?.Variant ?? fallback?.Variant;

        Tailor = priority?.Tailor ?? fallback?.Tailor;
        Blueprint = priority?.Blueprint ?? fallback?.Blueprint;
    }

    MagicBreadcrumbSettings ICanClone<MagicBreadcrumbSettings>.CloneUnder(MagicBreadcrumbSettings? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);


    #region WIP properties moved from specs

    /// <summary>
    /// If the current page should be included in the breadcrumb.
    ///
    /// Set to false for scenarios where you don't want to show the final page,
    /// or will use custom code to visualize differently.
    /// </summary>
    [JsonIgnore] // Marked as JsonIgnore to ensure we know that ATM there is no JSON support expected of this property
    public bool? WithActive { get; init; }

    internal bool WithActiveSafe => WithActive ?? true;

    /// <summary>
    /// If the home page should be included in the breadcrumb.
    /// This is special because the home page is usually not really "above" the others, but typically side-by side to other pages on the top level menu.
    ///
    /// Set to false, if you only want to show the breadcrumb starting at the level below home.
    /// </summary>
    [JsonIgnore] // Marked as JsonIgnore to ensure we know that ATM there is no JSON support expected of this property
    public bool? WithHome { get; init; }

    /// <summary>
    /// Maximum depth of the breadcrumb, defaults to 10.
    /// This is to ensure that we don't run into infinite loops.
    /// </summary>
    [JsonIgnore] // Marked as JsonIgnore to ensure we know that ATM there is no JSON support expected of this property
    public int? MaxDepth { get; init; }

    /// <summary>
    /// If the order of the Breadcrumb should be reversed.
    /// </summary>
    [JsonIgnore] // Marked as JsonIgnore to ensure we know that ATM there is no JSON support expected of this property
    public bool? Reverse { get; init; }

    /// <summary>
    /// List of pages to respect when creating the breadcrumb.
    /// Default is `null` - so it will just take all the pages.
    ///
    /// TODO: NAMING
    /// </summary>
    [JsonIgnore] // Marked as JsonIgnore to ensure we know that ATM there is no JSON support expected of this property
    public IEnumerable<IMagicPage>? Pages { get; init; } = null;

    // TODO: NAMING
    [JsonIgnore] // Marked as JsonIgnore to ensure we know that ATM there is no JSON support expected of this property
    public IMagicPage? Active { get; init; }

    #endregion

    /// <summary>
    /// A unique ID to identify the breadcrumb.
    /// </summary>
    public string? Id { get; init; }


    /// <summary>
    /// Determines if this breadcrumb should be shown.
    ///
    /// TODO: PROBABLY NOT IMPLEMENTED YET
    /// </summary>
    public bool? Show { get; init; }

    /// <summary>
    /// Start page of this breadcrumb - like home or another specific page.
    /// Can be
    /// - a specific ID
    /// - blank / null, current page
    /// </summary>
    public int? ActiveId { get; init; }


    public string? MenuId { get; init; }

    public string? Variant { get; init; } // TODO:


    [JsonIgnore] // Marked as JsonIgnore to ensure we know that ATM there is no JSON support expected of this property
    public IMagicPageTailor? Tailor { get; init; }

    IMagicPageTailor? IWith<IMagicPageTailor?>.WithData { get => Tailor; init => Tailor = value; }

    [JsonIgnore]
    public MagicBreadcrumbBlueprint? Blueprint { get; init; }

    MagicBreadcrumbBlueprint? IWith<MagicBreadcrumbBlueprint?>.WithData { get => Blueprint; init => Blueprint = value; }

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
    public new record Stabilized(MagicBreadcrumbSettings BreadcrumbSettings) : MagicSettings.Stabilized(BreadcrumbSettings)
    {
        public bool Reverse => BreadcrumbSettings.Reverse ?? DefaultReverse;
        public const bool DefaultReverse = false;

        public bool WithHome => BreadcrumbSettings.WithHome ?? DefaultWithHome;
        public const bool DefaultWithHome = true;

        public bool WithActive => BreadcrumbSettings.WithActive ?? DefaultWithActive;
        public const bool DefaultWithActive = true;

        public int MaxDepth => BreadcrumbSettings.MaxDepth ?? DefaultMaxDepth;
        public const int DefaultMaxDepth = 10;

        [field: AllowNull, MaybeNull]
        public string MenuId => field ??= BreadcrumbSettings.MenuId ?? SettingHelpers.RandomLongId(BreadcrumbSettings.Id);

    }

    #endregion

}