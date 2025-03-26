using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Menus;

/// <summary>
/// The raw settings for a menu, in a way which can be stored elsewhere.
///
/// This is later augmented with additional information which only code can provide, in the <see cref="MagicMenuSettings"/>
/// </summary>
/// <remarks>
/// This is implemented as an immutable record.
/// </remarks>
public record MagicMenuSettings : MagicSettings, IMagicPageSetSettings, ICanClone<MagicMenuSettings>, IWith<MagicMenuBlueprint?>
{
    /// <summary>
    /// Default constructor, so this record can be created anywhere.
    /// </summary>
    [PrivateApi]
    public MagicMenuSettings() { }

    private MagicMenuSettings(MagicMenuSettings? priority, MagicMenuSettings? fallback = default) : base(priority, fallback)
    {
        Id = priority?.Id ?? fallback?.Id;
        Show = priority?.Show ?? fallback?.Show;
        Pick = priority?.Pick ?? fallback?.Pick;
        Variant = priority?.Variant ?? fallback?.Variant;
        MenuId = priority?.MenuId ?? fallback?.MenuId;

        // Code-Only Settings
        Tailor = priority?.Tailor ?? fallback?.Tailor;
        Blueprint = priority?.Blueprint ?? fallback?.Blueprint;
        PagesSource = priority?.PagesSource ?? fallback?.PagesSource;
    }

    MagicMenuSettings ICanClone<MagicMenuSettings>.CloneUnder(MagicMenuSettings? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    /// <summary>
    /// A unique ID to identify the menu.
    /// Would be used for debugging but also to help in creating unique css-classes for collapsible menus
    ///
    /// Note: possibly not really used, except for the fallback menu ID
    /// </summary>
    public string? Id { get; init; }
    

    /// <summary>
    /// Determines if this navigation should be shown.
    /// Mainly used for standard menus which could be disabled through settings. 
    /// </summary>
    public bool? Show { get; init; }

    //// TODO: NOT YET IMPLEMENTED
    ///// <summary>
    ///// Exact list of pages to show in this menu.
    ///// TODO: MAYBE allow for negative numbers to remove them from the list?
    ///// </summary>
    //public List<int>? PageList { get; set; }

    /// <summary>
    /// Start page of this navigation - like home or another specific page.
    /// Can be
    /// - a specific ID
    /// - a CSV of IDs ???
    /// - `*` to indicate all pages on the specified level
    /// - `.` to indicate current page
    /// - blank / null, to use another start ???
    /// </summary>
    public string? Pick { get; init; }

    internal const char StartPageRootSlash = '/';
    internal const string DoubleSlash = "//";
    internal const char StartPageCurrent = '.';
    internal const string StartPageParent = "..";

    /// <summary>
    /// The menu variant to use, for example `horizontal` or `vertical`.
    /// Will only have an effect if the control showing the menu supports it.
    /// </summary>
    public string? Variant { get; init; }

    /// <summary>
    /// TODO: unclear, doesn't seem to be used? 
    /// </summary>
    public string? MenuId { get; }

    #region Code-Only Settings

    [JsonIgnore]
    public IMagicPageTailor? Tailor { get; init; }

    [JsonIgnore]
    public MagicMenuBlueprint? Blueprint { get; init; }

    MagicMenuBlueprint? IWith<MagicMenuBlueprint?>.WithData { get => Blueprint; init => Blueprint = value; }

    /// <summary>
    /// List of pages to respect when creating the breadcrumb.
    /// Default is `null` - so it will just take all the pages.
    ///
    /// TODO: NAMING
    /// </summary>
    [JsonIgnore]
    [PrivateApi("Not done yet, do not user")]
    public IEnumerable<IMagicPage>? PagesSource { get; init; }

    #endregion


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
    public new record Stabilized(MagicMenuSettings MenuSettings) : MagicSettings.Stabilized(MenuSettings)
    {
        public string Id => MenuSettings.Id ?? "TODO";

        public bool Show => MenuSettings.Show ?? DefaultShow;
        public const bool DefaultShow = true;

        public string Pick => MenuSettings.Pick ?? StartPageRootSlash.ToString();
        public string Variant => MenuSettings.Variant ?? "Horizontal";

        [field: AllowNull, MaybeNull]
        public string MenuId => field ??= MenuSettings.MenuId ?? SettingHelpers.RandomLongId(MenuSettings.Id);

        // TODO: still nullable... - SHOULD CHANGE
        public IMagicPageTailor? Tailor => MenuSettings.Tailor;

        [field: AllowNull, MaybeNull]
        public MagicMenuBlueprint Blueprint => field ??= MenuSettings.Blueprint ?? new();

    }


    #endregion


}