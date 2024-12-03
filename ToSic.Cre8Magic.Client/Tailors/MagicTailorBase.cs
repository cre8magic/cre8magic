using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Tailors;

public abstract class MagicTailorBase: IMagicTailor
{
    protected internal MagicTailorBase(TokenEngine tokens, Dictionary<string, MagicBlueprintPart> designSettings)
    {
        DesignSettings = designSettings;
        Tokens = tokens;
    }
    protected Dictionary<string, MagicBlueprintPart> DesignSettings { get; }


    protected MagicBlueprintPart? GetSettings(string name) =>
        DesignSettings.GetValueOrDefault(name);

    internal virtual TokenEngine Tokens { get; }

    protected virtual bool ParseTokens => true;

    protected string? ProcessTokens(string? value) => !ParseTokens
        ? value.EmptyAsNull()
        : Tokens.Parse(value).EmptyAsNull();

    public virtual string? Classes(string target) => ProcessTokens(GetSettings(target)?.Classes);

    public string? Value(string target) => ProcessTokens(GetSettings(target)?.Value);

    public string? Id(string name) => ProcessTokens(GetSettings(name)?.Id);

}