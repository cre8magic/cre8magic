using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Values;
using ToSic.Cre8magic.Tailors;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Themes.Settings;

/// <summary>
/// Constants and helpers related to creating Css and Css Classes.
///
/// If you change these, you must also update the SCSS files. 
/// </summary>
public partial record MagicThemeBlueprint: MagicBlueprint, ICanClone<MagicThemeBlueprint>
{
    // TODO: SHARE CONSTANTS WITH OTHER PLACE
    internal const string ThemePrefix = "theme";
    private const string PanePrefix = "pane";
    internal const string SettingFromDefaults = $"{ThemePrefix}-warning-this-is-from-defaults-you-should-set-your-own-value";

    private const string ContainerIdDefault = "module-[Module.Id]";
    internal const string ModulePrefixDefault = "module";

    #region Constructor and Clone

    [PrivateApi]
    public MagicThemeBlueprint() { }

    private MagicThemeBlueprint(MagicThemeBlueprint? priority, MagicThemeBlueprint? fallback = default)
        : base(priority, fallback)
    {
        PaneIsEmpty = priority?.PaneIsEmpty ?? fallback?.PaneIsEmpty;
    }

    MagicThemeBlueprint ICanClone<MagicThemeBlueprint>.CloneUnder(MagicThemeBlueprint? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    #endregion


    public MagicSettingOnOff? PaneIsEmpty { get; init; }

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
    public new record Stabilized(MagicThemeBlueprint ThemeBlueprint) : MagicBlueprint.Stabilized(ThemeBlueprint)
    {
        public MagicSettingOnOff PaneIsEmpty => ThemeBlueprint.PaneIsEmpty ?? new($"{PanePrefix}-is-empty");

        public new Dictionary<string, MagicBlueprintPart> Parts =>
            ThemeBlueprint.Parts ?? new(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "container", new()
                    {
                        Classes = $"{ThemePrefix}-page-language {SettingFromDefaults}",
                        IsPublished = new(null, $"{ModulePrefixDefault}-unpublished  {SettingFromDefaults}"),
                        IsAdmin = new($"{ThemePrefix}-admin-container  {SettingFromDefaults}"),
                        Id = ContainerIdDefault,
                    }
                },
            };

    }

    #endregion
}