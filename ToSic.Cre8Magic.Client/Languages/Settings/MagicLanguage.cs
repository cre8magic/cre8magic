using System.Globalization;
using ToSic.Cre8magic.Settings.Internal;

namespace ToSic.Cre8magic.Languages.Settings;

public record MagicLanguage: ICanClone<MagicLanguage>
{
    /// <summary>
    /// Empty constructor for deserialization
    /// </summary>
    public MagicLanguage() { }

    public MagicLanguage(MagicLanguage? priority, MagicLanguage? fallback = default)
    {
        Culture = priority?.Culture ?? fallback?.Culture;
        Label = priority?.Label ?? fallback?.Label;
        Description = priority?.Description ?? fallback?.Description;
    }

    public MagicLanguage CloneMerge(MagicLanguage? priority, bool forceCopy = false) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    public string? Culture { get; init; }

    /// <summary>
    /// Label to show for this culture.
    /// Will auto-default to first two characters. 
    /// </summary>
    public string? Label { get; init; }

    /// <summary>
    /// Description to show for this language.
    /// Will auto-default to the system name for this language. 
    /// </summary>
    public string? Description { get; init; }

    public bool IsActive => CultureInfo.CurrentUICulture.Name == Culture;

    // TODO: MAYBE additional options to only enable on certain roles...?
}