using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Menus;

public record MagicMenuBlueprintPart: MagicDesignSettingsPart, ICanClone<MagicMenuBlueprintPart>
{
    [PrivateApi]
    public MagicMenuBlueprintPart() { }

    private MagicMenuBlueprintPart(MagicMenuBlueprintPart? priority, MagicMenuBlueprintPart? fallback = default): base(priority, fallback)
    {
        ByLevel = priority?.ByLevel ?? fallback?.ByLevel;
        HasChildren = priority?.HasChildren ?? fallback?.HasChildren;
        IsDisabled = priority?.IsDisabled ?? fallback?.IsDisabled;
        InBreadcrumb = priority?.InBreadcrumb ?? fallback?.InBreadcrumb;
    }

    MagicMenuBlueprintPart ICanClone<MagicMenuBlueprintPart>.CloneUnder(MagicMenuBlueprintPart? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    /// <summary>
    /// List of classes to add on certain levels only.
    /// Use level -1 to specify classes to apply to all the remaining ones which are not explicitly listed.
    /// </summary>
    public Dictionary<int, string>? ByLevel { get; init; }

    /// <summary>
    /// Classes to add if this node is a parent (has-children).
    /// </summary>
    public MagicSettingOnOff? HasChildren { get; init; }

    /// <summary>
    /// Classes to add if the node is disabled.
    /// TODO: unclear why it's disabled, what would cause this...
    /// </summary>
    public MagicSettingOnOff? IsDisabled { get; init; }

    /// <summary>
    /// Classes to add if this node is in the path / breadcrumb of the current page.
    /// </summary>
    public MagicSettingOnOff? InBreadcrumb { get; init; }
}