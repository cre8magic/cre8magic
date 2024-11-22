using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Designers;

public abstract class MagicDesignerBase: IMagicDesigner
{
    protected internal MagicDesignerBase(MagicThemeContextFull context, Dictionary<string, MagicDesignSettings>? designSettings = default)
    {
        Context = context;
        DesignSettings = designSettings ?? context.ThemeDesignSettings.ByName;
        Tokens = context.PageTokens;
    }
    protected MagicThemeContextFull Context { get; }
    protected Dictionary<string, MagicDesignSettings> DesignSettings { get; }


    protected MagicDesignSettings? GetSettings(string name) =>
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