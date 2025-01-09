using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using ToSic.Cre8magic.Pages;
using ToSic.Cre8magic.Spells;
using ToSic.Cre8magic.Spells.Internal;

namespace ToSic.Cre8magic.Menus;

/// <summary>
/// The raw settings for a menu, in a way which can be stored elsewhere.
///
/// This is later augmented with additional information which only code can provide, in the <see cref="MagicMenuSpell"/>
/// </summary>
/// <remarks>
/// This is implemented as an immutable record.
/// </remarks>
public record MagicMenuSpell : MagicSpellBase, IMagicPageSetSettings, ICanClone<MagicMenuSpell>, IWith<MagicMenuBlueprint>
{
    /// <summary>
    /// Default constructor, so this record can be created anywhere.
    /// </summary>
    [PrivateApi]
    public MagicMenuSpell() { }

    private MagicMenuSpell(MagicMenuSpell? priority, MagicMenuSpell? fallback = default) : base(priority, fallback)
    {
        Id = priority?.Id ?? fallback?.Id;
        Show = priority?.Show ?? fallback?.Show;
        Pick = priority?.Pick ?? fallback?.Pick;
        Variant = priority?.Variant ?? fallback?.Variant;

        // Code-Only Settings
        Tailor = priority?.Tailor ?? fallback?.Tailor;
        Blueprint = priority?.Blueprint ?? fallback?.Blueprint;
        PagesSource = priority?.PagesSource ?? fallback?.PagesSource;
    }

    MagicMenuSpell ICanClone<MagicMenuSpell>.CloneUnder(MagicMenuSpell? priority, bool forceCopy) =>
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
    public bool? Show { get; init; }
    internal bool ShowSafe => Show ?? true;

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


    [field: AllowNull, MaybeNull]
    public string MenuId => field ??= SpellHelpers.RandomLongId(Id);

    #region Code-Only Settings

    [JsonIgnore]    // Not meant for JSON at all...
    public IMagicPageTailor? Tailor { get; init; }

    [JsonIgnore]
    public MagicMenuBlueprint? Blueprint { get; init; }

    [field: AllowNull, MaybeNull]
    internal MagicMenuBlueprint BlueprintSafe => field ??= Blueprint ?? MagicMenuBlueprint.Defaults.Fallback;

    MagicMenuBlueprint? IWith<MagicMenuBlueprint>.WithData { get => Blueprint; init => Blueprint = value; }

    /// <summary>
    /// List of pages to respect when creating the breadcrumb.
    /// Default is `null` - so it will just take all the pages.
    ///
    /// TODO: NAMING
    /// </summary>
    [JsonIgnore]    // Not meant for JSON at all...
    public IEnumerable<IMagicPage>? PagesSource { get; init; }

    #endregion


    internal static Defaults<MagicMenuSpell> Defaults = new()
    {
        Fallback = new()
        {
            Variant = "Horizontal",
            Pick = StartPageRootSlash.ToString(),
        },
        Foundation = new()
        {
            Variant = "Horizontal",
            Pick = StartPageRootSlash.ToString(),
        },
    };

}