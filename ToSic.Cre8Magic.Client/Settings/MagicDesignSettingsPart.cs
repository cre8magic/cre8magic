using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Settings;

/// <summary>
/// Anything that can define what classes it should have.
///
/// This is usually the base class for something that can also have more information.
/// </summary>
public record MagicDesignSettingsPart: ICanClone<MagicDesignSettingsPart>
{
    public MagicDesignSettingsPart() { }

    /// <summary>
    /// Internal because of inheritance, and we don't want it protected because that would be public.
    /// </summary>
    [PrivateApi]
    internal MagicDesignSettingsPart(MagicDesignSettingsPart? priority, MagicDesignSettingsPart? fallback = default)
    {
        Classes = priority?.Classes ?? fallback?.Classes;
        Value = priority?.Value ?? fallback?.Value;
        Id = priority?.Id ?? fallback?.Id;
        IsActive = priority?.IsActive ?? fallback?.IsActive;
        IsPublished = priority?.IsPublished ?? fallback?.IsPublished;
        IsAdmin = priority?.IsAdmin ?? fallback?.IsAdmin;
    }

    [PrivateApi]
    MagicDesignSettingsPart ICanClone<MagicDesignSettingsPart>.CloneUnder(MagicDesignSettingsPart? priority, bool forceCopy = false) =>
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
}