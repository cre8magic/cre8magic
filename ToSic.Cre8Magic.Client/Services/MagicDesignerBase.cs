using ToSic.Cre8magic.Designers;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Themes.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Client.Services;

public abstract class MagicDesignerBase: IMagicDesigner
{
    protected internal MagicDesignerBase(MagicThemeContextFull context)
    {
        Context = context;
    }
    protected MagicThemeContextFull Context { get; }


    protected MagicDesignSettings? GetSettings(string name) =>
        Context.ThemeDesignSettings.DesignSettings.TryGetValue(name, out var found) ? found : null;

    internal virtual TokenEngine Tokens => _tokens ??= Context.PageTokens;
    private TokenEngine? _tokens;

    protected virtual bool ParseTokens => true;

    protected string? ProcessTokens(string? value) => !ParseTokens
        ? value.EmptyAsNull()
        : Tokens.Parse(value).EmptyAsNull();

    public virtual string? Classes(string target) => ProcessTokens(GetSettings(target)?.Classes);

    public string? Value(string target) => ProcessTokens(GetSettings(target)?.Value);

    public string? Id(string name) => ProcessTokens(GetSettings(name)?.Id);

}