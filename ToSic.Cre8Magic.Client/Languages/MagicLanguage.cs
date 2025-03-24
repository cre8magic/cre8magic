using System.Globalization;
using ToSic.Cre8magic.Internal;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Utils;


namespace ToSic.Cre8magic.Languages;

/// <summary>
/// Describes a language inside Oqtane.
/// With Name, Label, Culture and Active-State.
/// </summary>
public record MagicLanguage: ICanClone<MagicLanguage>
{
    /// <summary>
    /// Empty constructor for deserialization
    /// </summary>
    [PrivateApi]
    public MagicLanguage() { }

    private MagicLanguage(MagicLanguage? priority, MagicLanguage? fallback = default)
    {
        Culture = priority?.Culture ?? fallback?.Culture ?? "en";
        Label = priority?.Label ?? fallback?.Label;
        Description = priority?.Description ?? fallback?.Description;
    }

    MagicLanguage ICanClone<MagicLanguage>.CloneUnder(MagicLanguage? priority, bool forceCopy) =>
        priority == null ? (forceCopy ? this with { } : this) : new(priority, this);

    public string Culture { get; init; } = "en";

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

    public bool IsActive => CultureInfo.CurrentUICulture.Name.EqInvariant(Culture);
}