using System.Text.Json.Serialization;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Debug;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Breadcrumb.Settings;

/// <summary>
/// Breadcrumb settings - either provided in code, or generated from JSON.
/// </summary>
/// <remarks>
/// NOTE that as of v0.2 the JSON variant is not in use.
/// </remarks>
public record MagicBreadcrumbSettings : SettingsWithInherit, IHasDebugSettings, IMagicPageSetSettings
{
    #region WIP properties moved from specs

    /// <summary>
    /// If the current page should be included in the breadcrumb.
    ///
    /// Set to false for scenarios where you don't want to show the final page,
    /// or will use custom code to visualize differently.
    /// </summary>
    [JsonIgnore] // Marked as JsonIgnore to ensure we know that ATM there is no JSON support expected of this property
    public bool WithActive { get; init; } = true;

    /// <summary>
    /// If the home page should be included in the breadcrumb.
    /// This is special because the home page is usually not really "above" the others, but typically side-by side to other pages on the top level menu.
    ///
    /// Set to false, if you only want to show the breadcrumb starting at the level below home.
    /// </summary>
    [JsonIgnore] // Marked as JsonIgnore to ensure we know that ATM there is no JSON support expected of this property
    public bool WithHome { get; init; } = true;

    [JsonIgnore] // Marked as JsonIgnore to ensure we know that ATM there is no JSON support expected of this property
    public IMagicPageDesigner? Designer { get; init; }

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
    public bool Reverse { get; init; } = false;

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
    /// Name to identify this configuration
    /// </summary>
    // TODO: REVIEW NAME
    public string? ConfigName { get; init; }

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

    // todo: name, maybe not on interface
    public NamedSettings<MagicBreadcrumbDesign>? DesignSettings { get; init; }

    public string MenuId => _menuId ??= SettingsUtils.RandomLongId(Id);
    private string? _menuId;

    public string Variant => ""; // TODO

    /// <summary>
    /// Defaults - these don't do anything, but we want to use this pattern for consistency.
    /// </summary>
    internal static Defaults<MagicBreadcrumbSettings> Defaults = new()
    {
        Fallback = new(),
        Foundation = new(),
    };
}