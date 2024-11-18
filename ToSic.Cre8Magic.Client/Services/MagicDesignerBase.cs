using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Client.Services;

public abstract class MagicDesignerBase: IMagicDesigner
{
    protected internal MagicDesignerBase(DesignerContextWip context, MagicAllSettings allSettings)
    {
        Context = context;
        AllSettings = allSettings;
    }
    protected DesignerContextWip Context { get; }
    public MagicAllSettings AllSettings { get; }


    protected MagicDesignSettings? GetSettings(string name) =>
        AllSettings?.ThemeDesignSettings.DesignSettings.GetInvariant(name);

    internal virtual TokenEngine Tokens => _tokens ??= Context.TokenEngineWip;
    private TokenEngine? _tokens;

    protected virtual bool ParseTokens => true;

    protected string? PostProcess(string? value)
    {
        if (!ParseTokens) return value.EmptyAsNull();
        return Tokens.Parse(value).EmptyAsNull();
    }

    public virtual string? Classes(string target) => PostProcess(GetSettings(target)?.Classes);

    public string? Value(string target) => PostProcess(GetSettings(target)?.Value);

    public string? Id(string name) => PostProcess(GetSettings(name)?.Id);

}