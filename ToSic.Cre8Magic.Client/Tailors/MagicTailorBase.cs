using ToSic.Cre8magic.Internal;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;


namespace ToSic.Cre8magic.Tailors;

public abstract class MagicTailorBase: IMagicTailor
{
    protected internal MagicTailorBase(TokenEngine tokens, Dictionary<string, MagicBlueprintPart> parts)
    {
        Parts = parts;
        Tokens = tokens;
    }
    protected Dictionary<string, MagicBlueprintPart> Parts { get; }


    protected MagicBlueprintPart? GetSettings(string name) =>
        Parts.GetValueOrDefault(name);

    internal virtual TokenEngine Tokens { get; }

    protected virtual bool ParseTokens => true;

    protected string? ProcessTokens(string? value) => !ParseTokens
        ? value.EmptyAsNull()
        : Tokens.Parse(value).EmptyAsNull();

    public virtual string? Classes(string target) => ProcessTokens(GetSettings(target)?.Classes);

    public virtual string? Value(string target) => ProcessTokens(GetSettings(target)?.Value);

    public virtual string? Id(string name) => ProcessTokens(GetSettings(name)?.Id);

}