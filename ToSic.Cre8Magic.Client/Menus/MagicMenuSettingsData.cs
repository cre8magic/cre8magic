using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Debug;

namespace ToSic.Cre8magic.Menus;

/// <summary>
/// The raw settings for a menu, in a way which can be stored elsewhere.
///
/// This is later augmented with additional information which only code can provide, in the <see cref="MagicMenuSettings"/>
/// </summary>
/// <remarks>
/// This is implemented as an immutable record.
/// </remarks>
public record MagicMenuSettingsData : SettingsWithInherit, IHasDebugSettings, IMagicPageSetSettings, ICanClone<MagicMenuSettingsData>
{
    /// <summary>
    /// Default constructor, so this record can be created anywhere.
    /// </summary>
    public MagicMenuSettingsData() { }

    [PrivateApi]
    internal MagicMenuSettingsData(MagicMenuSettingsData? priority, MagicMenuSettingsData? fallback = default) : base(priority, fallback)
    {
        Id = priority?.Id ?? fallback?.Id;
        Debug = priority?.Debug ?? fallback?.Debug;
        Display = priority?.Display ?? fallback?.Display;
        Depth = priority?.Depth ?? fallback?.Depth;
        Children = priority?.Children ?? fallback?.Children;
        Start = priority?.Start ?? fallback?.Start;
        Level = priority?.Level ?? fallback?.Level;
        Template = priority?.Template ?? fallback?.Template;
    }

    [PrivateApi]
    MagicMenuSettingsData ICanClone<MagicMenuSettingsData>.CloneUnder(MagicMenuSettingsData? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    /// <summary>
    /// A unique ID to identify the menu.
    /// Would be used for debugging but also to help in creating unique css-classes for collapsible menus
    /// </summary>
    public string? Id { get; init; }
    
    /// <inheritdoc />
    public MagicDebugSettings? Debug { get; init; }

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
    /// so if the navigation starts an level 2 then levelDepth 2 means to show levels 2 & 3 ??? verify
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
    public string? Template { get; init; }
    //public const string TemplateDefault = "Horizontal";


    public string MenuId => _menuId ??= SettingsUtils.RandomLongId(Id);
    private string? _menuId;

    public string Variant => Template ?? "";


    internal static Defaults<MagicMenuSettingsData> Defaults = new()
    {
        Fallback = new()
        {
            Template = "Horizontal",
            Start = StartPageRoot,
            Depth = 1, // MUST be specified on the fallback, otherwise some code will break
            //IsFounded = true,
        },
        Foundation = new()
        {
            Template = "Horizontal",
            Start = StartPageRoot,
            Depth = 0,
            //IsFounded = true,
        },
    };
}