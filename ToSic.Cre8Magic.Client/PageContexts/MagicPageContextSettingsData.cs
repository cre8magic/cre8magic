using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Settings.Internal.Debug;

namespace ToSic.Cre8magic.PageContexts;

/// <summary>
/// Magic Page Context Settings - Data.
///
/// This configures how the page context is rendered, and what classes are added to the body tag.
/// </summary>
public partial record MagicPageContextSettingsData: SettingsWithInherit, IHasDebugSettings, ICanClone<MagicPageContextSettingsData>
{
    #region Constructors and Cloning

    /// <summary>
    /// Empty constructor for serialization and creating new records.
    /// </summary>
    [PrivateApi]
    public MagicPageContextSettingsData() { }

    /// <summary>
    /// Clone constructor.
    /// </summary>
    [PrivateApi]
    internal MagicPageContextSettingsData(MagicPageContextSettingsData? priority, MagicPageContextSettingsData? fallback = default)
        : base(priority, fallback)
    {
        UseBodyTag = priority?.UseBodyTag ?? fallback?.UseBodyTag;

        ClassList = priority?.ClassList ?? fallback?.ClassList;
        PageIsHome = priority?.PageIsHome ?? fallback?.PageIsHome;
        TagId = priority?.TagId ?? fallback?.TagId;

        //Parts = MergeHelper.CloneMergeDictionaries(priority?.Parts, fallback?.Parts);


        Debug = priority?.Debug ?? fallback?.Debug;
    }

    /// <inheritdoc />
    [PrivateApi]
    MagicPageContextSettingsData ICanClone<MagicPageContextSettingsData>.CloneUnder(MagicPageContextSettingsData? priority, bool forceCopy = false) =>
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



    //// Note: not in use ATM
    ///// <summary>
    ///// Custom values / classes as you need them in your code
    ///// </summary>
    //[JsonConverter(typeof(CaseInsensitiveDictionaryConverter<MagicDesignSettingsPart>))]
    //public Dictionary<string, MagicDesignSettingsPart> Parts { get; init; } = new();


    public MagicDebugSettings? Debug { get; init; }

}