using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Breadcrumb.Settings;

public record MagicBreadcrumbDesign : DesignSetting, ICanClone<MagicBreadcrumbDesign>
{
    public MagicBreadcrumbDesign() { }

    public MagicBreadcrumbDesign(MagicBreadcrumbDesign? priority, MagicBreadcrumbDesign? fallback = default)
        : base(priority, fallback)
    {
        HasChildren = fallback?.HasChildren?.CloneMerge(priority?.HasChildren) ?? priority?.HasChildren;
        IsDisabled = fallback?.IsDisabled?.CloneMerge(priority?.IsDisabled) ?? priority?.IsDisabled;
    }

    public MagicBreadcrumbDesign CloneMerge(MagicBreadcrumbDesign? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);


    ///// <summary>
    ///// List of classes to add on certain levels only.
    ///// Use level -1 to specify classes to apply to all the remaining ones which are not explicitly listed.
    ///// </summary>
    //public Dictionary<int, string>? ByLevel { get; set; }

    /// <summary>
    /// Classes to add if this node is a parent (has-children).
    /// </summary>
    public PairOnOff? HasChildren { get; init; }

    /// <summary>
    /// Classes to add if the node is disabled.
    /// TODO: unclear why it's disabled, what would cause this...
    /// </summary>
    public PairOnOff? IsDisabled { get; init; }

    ///// <summary>
    ///// Classes to add if this node is in the path / breadcrumb of the current page.
    ///// </summary>
    //public PairOnOff? InBreadcrumb { get; set; }
}