using Oqtane.Models;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Containers;

internal class MagicContainerDesigner(DesignerContextWip context, MagicAllSettings allSettings, Module module) : MagicDesignerBase(context, allSettings)
{
    internal override TokenEngine Tokens => _tokens ??= context.TokenEngineWip.Expanded(new ModuleTokens(module));
    private TokenEngine? _tokens;

    public override string? Classes(string tag)
    {
        if (GetSettings(tag) is not { } styles) return null;
        var value = CombineWithModuleClasses(styles);
        return PostProcess(value);
    }


    /// <summary>
    /// Replace for container design rules
    /// </summary>
    /// <param name="styles"></param>
    /// <returns></returns>
    private string CombineWithModuleClasses(MagicDesignSettings styles)
    {
        var value =  string.Join(" ", new[]
        {
            styles.Classes,
            styles.IsPublished.Get(module.IsPublished()),      // Info-Class if not published
            styles.IsAdmin.Get(module.ForceAdminContainer())   // Info-class if admin module
        }.Where(s => s.HasValue()));

        return value;
    }

}