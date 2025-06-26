using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Internal;
using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Tokens;

/// <summary>
/// Basic token engine which takes a list of token replacers and runs them.
/// In the future, should be a bit more modern using a token parser and token providers
/// similar to 2sxc.
/// But ATM there are only ca. 10 tokens so the current model is probably sufficient
/// </summary>
public class TokenEngine: ITokenReplace
{
    public const string NameIdConst = nameof(TokenEngine);
    public string NameId => NameIdConst;

    #region Constructors and Child-Makers

    public TokenEngine() { }

    public TokenEngine(List<ITokenReplace> parsers) => Parsers = parsers.AsReadOnly();

    public ReadOnlyCollection<ITokenReplace> Parsers { get; } = new List<ITokenReplace>().AsReadOnly();

    public TokenEngine Expanded(ITokenReplace add) => new(Parsers.Concat(new List<ITokenReplace> { add }).ToList());

    #endregion

    public TokenEngine CloneWith(IMagicPage page) =>
        Parsers.FirstOrDefault(p => p.NameId == PageTokens.NameIdConstant) is not PageTokens originalPageTokens
            // Might not have any sources, or not a page-source, in which case we just create it.
            ? new([new PageTokens(page)])
            // In this case the original page tokens - which may have additional configuration - should be cloned
            : SwapParser(originalPageTokens.Clone(page));

    private TokenEngine SwapParser(ITokenReplace replacement)
    {
        // Create new list preserving the initial order
        var newParsers = Parsers
            .Select(p => p.NameId == replacement.NameId ? replacement : p)
            .ToList();

        // Determine if we replaced it, otherwise append
        if (newParsers.All(p => p.NameId != replacement.NameId))
            newParsers.Add(replacement);
        return new(newParsers);
    }


    [return: NotNullIfNotNull("value")]
    public string? Parse(string? value)
    {
        if (!value.HasValue() || !value.Contains(MagicTokens.PlaceholderMarker))
            return value;
        foreach (var p in Parsers)
        {
            value = p.Parse(value);
            if (value == null || !value.Contains(MagicTokens.PlaceholderMarker))
                return value;
        }

        return value;
    }
}