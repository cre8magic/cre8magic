using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Menus;

public record MagicMenuDesignSettings: MagicDesignSettings, ICanClone<MagicMenuDesignSettings>
{
    public MagicMenuDesignSettings() { }

    public MagicMenuDesignSettings(MagicMenuDesignSettings? priority, MagicMenuDesignSettings? fallback = default): base(priority, fallback)
    {
        ByLevel = priority?.ByLevel ?? fallback?.ByLevel;
        HasChildren = priority?.HasChildren ?? fallback?.HasChildren;
        IsDisabled = priority?.IsDisabled ?? fallback?.IsDisabled;
        InBreadcrumb = priority?.InBreadcrumb ?? fallback?.InBreadcrumb;
    }

    public MagicMenuDesignSettings CloneWith(MagicMenuDesignSettings? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    /// <summary>
    /// List of classes to add on certain levels only.
    /// Use level -1 to specify classes to apply to all the remaining ones which are not explicitly listed.
    /// </summary>
    public Dictionary<int, string>? ByLevel { get; init; }

    /// <summary>
    /// Classes to add if this node is a parent (has-children).
    /// </summary>
    public PairOnOff? HasChildren { get; init; }

    /// <summary>
    /// Classes to add if the node is disabled.
    /// TODO: unclear why it's disabled, what would cause this...
    /// </summary>
    public PairOnOff? IsDisabled { get; init; }

    /// <summary>
    /// Classes to add if this node is in the path / breadcrumb of the current page.
    /// </summary>
    public PairOnOff? InBreadcrumb { get; init; }
}