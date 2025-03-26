using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Values;

namespace ToSic.Cre8magic.PageContexts;

/// <summary>
/// Magic Page Context Settings - Data.
///
/// This configures how the page context is rendered, and what classes are added to the body tag.
/// </summary>
public record MagicPageContextSettings: MagicSettings, ICanClone<MagicPageContextSettings>
{
    #region Constructors and Cloning

    /// <summary>
    /// Empty constructor for serialization and creating new records.
    /// </summary>
    [PrivateApi]
    public MagicPageContextSettings() { }

    /// <summary>
    /// Clone constructor.
    /// </summary>
    private MagicPageContextSettings(MagicPageContextSettings? priority, MagicPageContextSettings? fallback = default)
        : base(priority, fallback)
    {
        UseBodyTag = priority?.UseBodyTag ?? fallback?.UseBodyTag;

        ClassList = priority?.ClassList ?? fallback?.ClassList;
        PageIsHome = priority?.PageIsHome ?? fallback?.PageIsHome;
        TagId = priority?.TagId ?? fallback?.TagId;
        Classes = priority?.Classes ?? fallback?.Classes;
        AddDefaults = priority?.AddDefaults ?? fallback?.AddDefaults;
    }

    /// <inheritdoc />
    MagicPageContextSettings ICanClone<MagicPageContextSettings>.CloneUnder(MagicPageContextSettings? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    #endregion

    /// <summary>
    /// If true, the body tag will be used to add classes and other attributes.
    /// This is done using JavaScript interop.
    /// If false, it will use a div around the content.
    /// </summary>
    public bool? UseBodyTag { get; init; }

    /// <summary>
    /// List of classes to add for the context.
    /// Should usually contain placeholders.
    /// </summary>
    public string[]? ClassList { get; init; }

    /// <summary>
    /// Classes to use if the page is the home page - or not.
    /// </summary>
    public MagicSettingOnOff? PageIsHome { get; init; }

    /// <summary>
    /// The ID to use for the tag when <see cref="UseBodyTag"/> is false.
    /// </summary>
    public string? TagId { get; init; }

    /// <summary>
    /// If true, will automatically add a series of common classes with information about the page, site, language etc.
    /// This adheres to a common naming convention and is recommended.
    /// </summary>
    /// <remarks>
    /// If not specified, will default to `true` if the class-list is empty.
    /// </remarks>
    public bool? AddDefaults { get; init; }


    public string? Classes { get; init; }


    #region Internal Reader

    [PrivateApi]
    public Stabilized GetStable() => new(this);

    /// <summary>
    /// Experimental 2025-03-25 2dm
    /// Purpose is to allow all settings to be nullable, but have a robust reader that will always return a value,
    /// so that the code using the values doesn't need to check for nulls.
    /// </summary>
    [PrivateApi]
    public new record Stabilized(MagicPageContextSettings PageContextSettings) : MagicSettings.Stabilized(PageContextSettings)
    {
        public bool UseBodyTag => PageContextSettings.UseBodyTag ?? DefaultUseBodyTag;
        internal const bool DefaultUseBodyTag = true;

        [field: AllowNull, MaybeNull]
        public string[] ClassList => field ??=
        [
            ..PageContextSettings.ClassList ?? [],
            ..UseDefaults ? MagicPageContextConstants.RecommendedClassList : []
        ];

        public MagicSettingOnOff PageIsHome => PageContextSettings.PageIsHome
                                               ?? new(UseDefaults ? MagicPageContextConstants.RecommendedPageIsHome : null);

        public string TagId => PageContextSettings.TagId ?? DefaultTagId;
        internal const string DefaultTagId = "cre8magic-root";

        public string Classes => PageContextSettings.Classes ?? string.Empty;

        public bool UseDefaults => PageContextSettings.AddDefaults ?? DefaultUseDefaults;
        internal const bool DefaultUseDefaults = true;
    }

    #endregion
}