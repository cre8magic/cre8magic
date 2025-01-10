using ToSic.Cre8magic.Spells;
using ToSic.Cre8magic.Spells.Internal;
using ToSic.Cre8magic.Spells.Values;

namespace ToSic.Cre8magic.PageContexts;

/// <summary>
/// Magic Page Context Settings - Data.
///
/// This configures how the page context is rendered, and what classes are added to the body tag.
/// </summary>
public partial record MagicPageContextSpell: MagicSpellBase, ICanClone<MagicPageContextSpell>
{
    #region Constructors and Cloning

    /// <summary>
    /// Empty constructor for serialization and creating new records.
    /// </summary>
    [PrivateApi]
    public MagicPageContextSpell() { }

    /// <summary>
    /// Clone constructor.
    /// </summary>
    private MagicPageContextSpell(MagicPageContextSpell? priority, MagicPageContextSpell? fallback = default)
        : base(priority, fallback)
    {
        UseBodyTag = priority?.UseBodyTag ?? fallback?.UseBodyTag;

        ClassList = priority?.ClassList ?? fallback?.ClassList;
        PageIsHome = priority?.PageIsHome ?? fallback?.PageIsHome;
        TagId = priority?.TagId ?? fallback?.TagId;
        Classes = priority?.Classes ?? fallback?.Classes;
    }

    /// <inheritdoc />
    MagicPageContextSpell ICanClone<MagicPageContextSpell>.CloneUnder(MagicPageContextSpell? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    #endregion


    /// <summary>
    /// If true, the body tag will be used to add classes and other attributes.
    /// If false, it will use a div around the content.
    /// </summary>
    public bool? UseBodyTag { get; init; }
    internal bool UseBodyTagSafe => UseBodyTag ?? false;

    /// <summary>
    /// List of classes to add for the context.
    /// Should usually contain placeholders.
    /// </summary>
    public string[]? ClassList { get; init; }

    /// <summary>
    /// Classes to use if the page is the home page - or not.
    /// </summary>
    public MagicSettingOnOff? PageIsHome { get; init; }

    public string? TagId { get; init; }


    public string? Classes { get; init; }

}