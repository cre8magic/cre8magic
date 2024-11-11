using ToSic.Cre8magic.Client.Settings.Internal;

namespace ToSic.Cre8magic.Client.Breadcrumb.Settings;

public class MagicBreadcrumbSettings : SettingsWithInherit, IHasDebugSettings
{
    /// <summary>
    /// Empty constructor is important for JSON deserialization
    /// </summary>
    public MagicBreadcrumbSettings() { }

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
    public int? Start { get; init; }

    // todo: name, maybe not on interface
    public NamedSettings<MagicBreadcrumbDesign>? DesignSettings { get; init; }

    public string BreadcrumbId => _breadcrumbId ??= SettingsUtils.RandomLongId(Id);
    private string? _breadcrumbId;

    private static readonly MagicBreadcrumbSettings FbAndF = new()
    {
        Start = null,
    };

    internal static Defaults<MagicBreadcrumbSettings> Defaults = new()
    {
        Fallback = FbAndF,
        Foundation = FbAndF,
    };
}