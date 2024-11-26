using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Designers;

public abstract class MagicDesignerBase: IMagicDesigner
{
    protected internal MagicDesignerBase(CmThemeContextFull context, Dictionary<string, MagicDesignSettingsPart>? designSettings = default)
    {
        Context = context;
        DesignSettings = designSettings ?? context.ThemeDesignSettings.Parts;
        Tokens = context.PageTokens;
    }
    protected CmThemeContextFull Context { get; }
    protected Dictionary<string, MagicDesignSettingsPart> DesignSettings { get; }


    protected MagicDesignSettingsPart? GetSettings(string name) =>
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