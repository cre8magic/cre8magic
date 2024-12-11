using System.Text.Json.Serialization;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Spells.Internal;
using ToSic.Cre8magic.Spells.Settings;
using ToSic.Cre8magic.Tailors.Internal;

namespace ToSic.Cre8magic.Tailors;

/// <summary>
/// Anything that can define what classes it should have.
///
/// This is usually the base class for something that can also have more information.
/// </summary>
[JsonConverter(typeof(BlueprintPartJsonConverter))]
public record MagicBlueprintPart: ICanClone<MagicBlueprintPart>
{
    [PrivateApi]
    public MagicBlueprintPart() { }

    /// <summary>
    /// Internal because of inheritance, and we don't want it protected because that would be public.
    /// </summary>
    internal MagicBlueprintPart(MagicBlueprintPart? priority, MagicBlueprintPart? fallback = default)
    {
        Classes = priority?.Classes ?? fallback?.Classes;
        Value = priority?.Value ?? fallback?.Value;
        Id = priority?.Id ?? fallback?.Id;
        IsActive = priority?.IsActive ?? fallback?.IsActive;
        IsPublished = priority?.IsPublished ?? fallback?.IsPublished;
        IsAdmin = priority?.IsAdmin ?? fallback?.IsAdmin;

        // Mostly for menus and breadcrumbs
        ByLevel = priority?.ByLevel ?? fallback?.ByLevel;
        HasChildren = priority?.HasChildren ?? fallback?.HasChildren;
        IsDisabled = priority?.IsDisabled ?? fallback?.IsDisabled;
        InBreadcrumb = priority?.InBreadcrumb ?? fallback?.InBreadcrumb;

    }

    MagicBlueprintPart ICanClone<MagicBlueprintPart>.CloneUnder(MagicBlueprintPart? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    /// <summary>
    /// Classes which are applied to all the tags of this type
    /// </summary>
    public string? Classes { get; init; }

    /// <summary>
    /// Special key to get a value - for non-css settings
    /// </summary>
    public string? Value { get; init; }


    public string? Id { get; init; }

    /// <summary>
    /// Classes to apply if this thing is active.
    /// For example, the current page or language. 
    /// </summary>
    public MagicSettingOnOff? IsActive { get; init; }

    #region Settings which are ATM for containers only

    /// <summary>
    /// If something is published or not, usually just for Containers
    /// </summary>
    public MagicSettingOnOff? IsPublished { get; init; }

    /// <summary>
    /// If a module is admin or not, usually just for containers
    /// </summary>
    public MagicSettingOnOff? IsAdmin { get; init; }

    #endregion

    #region Settings for Menus and Breadcrumbs


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

    #endregion

    /// <summary>
    /// Special Class just for json serialization, which will have the values but not the converter
    /// </summary>
    internal record NoJsonConverter : MagicBlueprintPart
    {
        public NoJsonConverter() { }    // for Deserialization
        public NoJsonConverter(MagicBlueprintPart original) : base(original) { } // For Serialization; explicit constructor, so original doesn't become a property
    }

}