using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Menus;

/// <summary>
/// The raw settings for a menu, in a way which can be stored elsewhere.
///
/// This is later augmented with additional information which only code can provide, in the <see cref="MagicMenuSettings"/>
/// </summary>
/// <remarks>
/// This is implemented as an immutable record.
/// </remarks>
public record MagicMenuSettings : MagicSettingsBase, IMagicPageSetSettings, ICanClone<MagicMenuSettings>
{
    /// <summary>
    /// Default constructor, so this record can be created anywhere.
    /// </summary>
    [PrivateApi]
    public MagicMenuSettings() { }

    private MagicMenuSettings(MagicMenuSettings? priority, MagicMenuSettings? fallback = default) : base(priority, fallback)
    {
        Id = priority?.Id ?? fallback?.Id;
        Display = priority?.Display ?? fallback?.Display;
        Depth = priority?.Depth ?? fallback?.Depth;
        Children = priority?.Children ?? fallback?.Children;
        Start = priority?.Start ?? fallback?.Start;
        Level = priority?.Level ?? fallback?.Level;
        Variant = priority?.Variant ?? fallback?.Variant;

        // Code-Only Settings
        Designer = priority?.Designer ?? fallback?.Designer;
        DesignSettings = priority?.DesignSettings ?? fallback?.DesignSettings;
        Pages = priority?.Pages ?? fallback?.Pages;
    }

    MagicMenuSettings ICanClone<MagicMenuSettings>.CloneUnder(MagicMenuSettings? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    /// <summary>
    /// A unique ID to identify the menu.
    /// Would be used for debugging but also to help in creating unique css-classes for collapsible menus
    /// </summary>
    public string? Id { get; init; }
    

    /// <summary>
    /// Determines if this navigation should be shown.
    /// Mainly used for standard menus which could be disabled through settings. 
    /// </summary>
    // TODO: REVIEW NAME - Show would probably be better!
    public bool? Display { get; init; } = DisplayDefault;
    public const bool DisplayDefault = true;

    /// <summary>
    /// How many level deep the navigation should show.
    /// The number is ??? relative,
    /// so if the navigation starts a level 2 then levelDepth 2 means to show levels 2 and 3 ??? verify
    /// </summary>
    public int? Depth { get; init; }
    //public const int DepthFallback = 1;

    /// <summary>
    /// Levels to skip from the initial stating point.
    /// - 0 means don't skip any, so if we're starting at the root, show that level
    /// - 1 means skip the first level, so if we're starting at the root, show the children
    /// See inspiration context from DDRMenu https://www.dnnsoftware.com/wiki/ddrmenu-reference-guide
    /// in DDR it was called 'skip' but it didn't make sense IMHO
    /// TODO: possibly rename to StartOnChildren or StartSkip?
    /// </summary>
    public bool? Children { get; init; }
    public const bool ChildrenFallback = default;

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
    public string? Start { get; init; }
    public const string StartPageRoot = "*";
    public const string StartPageRoot2 = "/";
    public const string StartPageCurrent = ".";

    /// <summary>
    /// The level this menu should start from.
    /// - `0` is the top level (default)
    /// - `1` is the top level containing home and other pages
    /// - `-1` is one level up from the current node
    /// - `-2` is two levels up from the current node
    /// </summary>
    public int? Level { get; init; }
    public const int StartLevelFallback = default;

    /// <summary>
    /// The template to use - horizontal/vertical
    /// </summary>
    public string? Variant { get; init; }


    [field: AllowNull, MaybeNull]
    public string MenuId => field ??= SettingsUtils.RandomLongId(Id);

    #region Code-Only Settings

    [JsonIgnore]    // Not meant for JSON at all...
    public IMagicPageDesigner? Designer { get; init; }

    [JsonIgnore]
    public MagicMenuDesignSettings? DesignSettings { get; init; } = new();

    /// <summary>
    /// List of pages to respect when creating the breadcrumb.
    /// Default is `null` - so it will just take all the pages.
    ///
    /// TODO: NAMING
    /// </summary>
    [JsonIgnore]    // Not meant for JSON at all...
    public IEnumerable<IMagicPage>? Pages { get; init; }

    #endregion


    internal static Defaults<MagicMenuSettings> Defaults = new()
    {
        Fallback = new()
        {
            Variant = "Horizontal",
            Start = StartPageRoot,
            Depth = 1, // MUST be specified on the fallback, otherwise some code will break
        },
        Foundation = new()
        {
            Variant = "Horizontal",
            Start = StartPageRoot,
            Depth = 0,
        },
    };
}