using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Client.Services;

internal abstract class MagicDesignerBase: MagicServiceWithSettingsBase, IMagicDesigner
{
    //protected internal MagicDesignerBase() {}

    protected internal MagicDesignerBase(MagicAllSettings allSettings) => InitSettings(allSettings);

    protected abstract DesignSetting? GetSettings(string name);

    protected virtual TokenEngine Tokens => _tokens ??= AllSettings.Tokens;
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