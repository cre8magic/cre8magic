using Oqtane.Models;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Containers;

internal class MagicContainerDesigner(MagicAllSettings allSettings, Module module) : MagicDesignerBase(allSettings)
{
    protected override TokenEngine Tokens => _tokens ??= AllSettings.Tokens.Expanded(new ModuleTokens(module));
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

    protected override MagicDesignSettings? GetSettings(string name) =>
        AllSettings?.ThemeDesign.DesignSettings.GetInvariant(name);

}