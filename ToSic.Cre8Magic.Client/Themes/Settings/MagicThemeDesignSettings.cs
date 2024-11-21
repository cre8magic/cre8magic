using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Themes.Settings;

/// <summary>
/// Constants and helpers related to creating Css and Css Classes.
///
/// If you change these, you must also update the SCSS files. 
/// </summary>
public record MagicThemeDesignSettings: SettingsWithInherit, ICanClone<MagicThemeDesignSettings>
{
    public MagicThemeDesignSettings() { }

    public MagicThemeDesignSettings(MagicThemeDesignSettings? priority, MagicThemeDesignSettings? fallback = default)
        : base(priority, fallback)
    {
        MagicContext = priority?.MagicContext ?? fallback?.MagicContext ?? [];
        PageIsHome = priority?.PageIsHome ?? fallback?.PageIsHome;
        PaneIsEmpty = priority?.PaneIsEmpty ?? fallback?.PaneIsEmpty;
        MagicContextTagId = priority?.MagicContextTagId ?? fallback?.MagicContextTagId;

        // TODO: #NamedSettings
        DesignSettings = priority?.DesignSettings ?? fallback?.DesignSettings ?? new();
    }

    public MagicThemeDesignSettings CloneUnder(MagicThemeDesignSettings? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);



    private const string SitePrefixDefault = "site";
    private const string PagePrefixDefault = "page";
    internal const string MainPrefix = "theme";
    private const string PanePrefixDefault = "pane";
    private const string MenuLevelPrefixDefault = "nav-level";
    private const string BodyDivId = "cre8magic-root";
    internal const string SettingFromDefaults = $"{MainPrefix}-warning-this-is-from-defaults-you-should-set-your-own-value";

    private static string[] MagicContextDefaults =
    [
        //1.2 Set the page-### class
        $"{PagePrefixDefault}-{MagicTokens.PageId}",
        //1.4 Set the page-root-### class
        $"{PagePrefixDefault}-root-{MagicTokens.PageRootId}",
        //1.3 Set the page-parent-### class
        $"{PagePrefixDefault}-parent-{MagicTokens.PageParentId}",
        //2 Set site-### class
        $"{SitePrefixDefault}-{MagicTokens.SiteId}",
        //3 Set the nav-level-### class
        $"{MenuLevelPrefixDefault}-{MagicTokens.MenuLevel}",
        //5.1 Set the theme-variation- class
        $"{MainPrefix}-variation-{MagicTokens.LayoutVariation}",

        //5.2 Set the theme-mainnav-variation- class to align the menu
        $"{MainPrefix}-mainnav-variation-right",

        // Debug info so we know the defaults were used
        SettingFromDefaults
    ];


    public string[] MagicContext { get; init; } = [];

    public PairOnOff? PageIsHome { get; init; }

    public PairOnOff? PaneIsEmpty { get; init; }

    public string? MagicContextTagId { get; init; }

    /// <summary>
    /// Custom values / classes as you need them in your code
    /// TODO: probably rename to "data"
    /// </summary>
    public NamedSettings<MagicDesignSettings> DesignSettings { get; init; } = new();

    // TODO: initialize with real properties, so the defaults don't already contain something?

    private const string ContainerIdDefault = "module-[Module.Id]";
    internal const string ModulePrefixDefault = "module";


    internal static Defaults<MagicThemeDesignSettings> Defaults = new()
    {
        Fallback = new()
        {
            MagicContext = MagicContextDefaults,
            PageIsHome = new($"{PagePrefixDefault}-is-home"),
            PaneIsEmpty = new($"{PanePrefixDefault}-is-empty"),
            MagicContextTagId = BodyDivId,
            DesignSettings = new()
            {
                { "languages-li", new() { IsActive = new($"active {SettingFromDefaults}") } },
                {
                    "container", new()
                    {
                        Classes = $"{MainPrefix}-page-language {SettingFromDefaults}",
                        IsPublished = new(null,
                            $"{ModulePrefixDefault}-unpublished  {SettingFromDefaults}"),
                        IsAdmin = new($"{MainPrefix}-admin-container  {SettingFromDefaults}"),
                        Id = ContainerIdDefault,
                    }
                },


            }
        },
        Foundation = new()
        {
            MagicContext = [],
            PageIsHome = new(),
            PaneIsEmpty = new(),
            MagicContextTagId = BodyDivId,
            DesignSettings = new()
            {
                { "container", new() { Id = ContainerIdDefault } },
            }
        },
    };
}