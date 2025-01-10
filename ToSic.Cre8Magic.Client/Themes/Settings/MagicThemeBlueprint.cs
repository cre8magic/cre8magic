using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Values;
using ToSic.Cre8magic.Tailors;

namespace ToSic.Cre8magic.Themes.Settings;

/// <summary>
/// Constants and helpers related to creating Css and Css Classes.
///
/// If you change these, you must also update the SCSS files. 
/// </summary>
public partial record MagicThemeBlueprint: MagicBlueprint, ICanClone<MagicThemeBlueprint>
{
    [PrivateApi]
    public MagicThemeBlueprint() { }

    private MagicThemeBlueprint(MagicThemeBlueprint? priority, MagicThemeBlueprint? fallback = default)
        : base(priority, fallback)
    {
        PaneIsEmpty = priority?.PaneIsEmpty ?? fallback?.PaneIsEmpty;
    }

    MagicThemeBlueprint ICanClone<MagicThemeBlueprint>.CloneUnder(MagicThemeBlueprint? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    
    public MagicSettingOnOff? PaneIsEmpty { get; init; }
}