using System.Text.Json.Serialization;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Debug;

namespace ToSic.Cre8magic.Breadcrumbs;

/// <summary>
/// Breadcrumb settings - either provided in code, or generated from JSON.
/// </summary>
/// <remarks>
/// NOTE that as of v0.2 the JSON variant is not in use.
/// </remarks>
public record MagicBreadcrumbSettings : MagicSettingsBase, IHasDebugSettings, IMagicPageSetSettings, ICanClone<MagicBreadcrumbSettings>, IWith<IMagicPageDesigner?>
{
    public MagicBreadcrumbSettings() { }

    /// <summary>
    /// Cloning constructor
    /// </summary>
    [PrivateApi]
    private MagicBreadcrumbSettings(MagicBreadcrumbSettings? priority, MagicBreadcrumbSettings? fallback = default): base(priority, fallback)
    {
        WithActive = priority?.WithActive ?? fallback?.WithActive ?? Defaults.Fallback.WithActive;
        WithHome = priority?.WithHome ?? fallback?.WithHome ?? Defaults.Fallback.WithHome;
        MaxDepth = priority?.MaxDepth ?? fallback?.MaxDepth ?? Defaults.Fallback.MaxDepth;
        Reverse = priority?.Reverse ?? fallback?.Reverse ?? Defaults.Fallback.Reverse;
        Pages = priority?.Pages ?? fallback?.Pages;
        Active = priority?.Active ?? fallback?.Active;
        Id = priority?.Id ?? fallback?.Id;
        Debug = priority?.Debug ?? fallback?.Debug;
        Display = priority?.Display ?? fallback?.Display ?? Defaults.Fallback.Display;
        ActiveId = priority?.ActiveId ?? fallback?.ActiveId;
        Variant = priority?.Variant ?? fallback?.Variant;

        Designer = priority?.Designer ?? fallback?.Designer;
        DesignSettings = priority?.DesignSettings ?? fallback?.DesignSettings;
    }

    [PrivateApi]
    MagicBreadcrumbSettings ICanClone<MagicBreadcrumbSettings>.CloneUnder(MagicBreadcrumbSettings? priority, bool forceCopy = false) =>
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
    internal bool WithHomeSafe => WithHome ?? true;

    /// <summary>
    /// Maximum depth of the breadcrumb, defaults to 10.
    /// This is to ensure that we don't run into infinite loops.
    /// </summary>
    [JsonIgnore] // Marked as JsonIgnore to ensure we know that ATM there is no JSON support expected of this property
    public int MaxDepth { get; init; } = 10;

    /// <summary>
    /// If the order of the Breadcrumb should be reversed.
    /// </summary>
    [JsonIgnore] // Marked as JsonIgnore to ensure we know that ATM there is no JSON support expected of this property
    public bool? Reverse { get; init; }
    internal bool ReverseSafe => Reverse ?? false;

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


    /// <inheritdoc />
    public MagicDebugSettings? Debug { get; init; }

    /// <summary>
    /// Determines if this breadcrumb should be shown.
    /// </summary>
    // TODO: REVIEW NAME - Show would probably be better!
    public bool? Display { get; init; } = DisplayDefault;
    public const bool DisplayDefault = true;

    /// <summary>
    /// Start page of this breadcrumb - like home or another specific page.
    /// Can be
    /// - a specific ID
    /// - blank / null, current page
    /// </summary>
    public int? ActiveId { get; init; }


    public string MenuId => _menuId ??= SettingsUtils.RandomLongId(Id);
    private string? _menuId;

    public string? Variant { get; init; } // TODO:


    [JsonIgnore] // Marked as JsonIgnore to ensure we know that ATM there is no JSON support expected of this property
    public IMagicPageDesigner? Designer { get; init; }

    IMagicPageDesigner? IWith<IMagicPageDesigner?>.WithData { get => Designer; init => Designer = value; }

    [JsonIgnore]
    public MagicBreadcrumbDesignSettings? DesignSettings { get; init; }


    /// <summary>
    /// Defaults - these don't do anything, but we want to use this pattern for consistency.
    /// </summary>
    internal static Defaults<MagicBreadcrumbSettings> Defaults = new()
    {
        Fallback = new(),
        Foundation = new(),
    };

}