using Oqtane.Models;
using ToSic.Cre8magic.Settings.Values.Internal;
using ToSic.Cre8magic.Tailors;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils;
using ToSic.Cre8magic.Utils.Internal;

namespace ToSic.Cre8magic.Containers;

public class MagicContainerTailor(CmThemeContextFull context, Module module, MagicContainerBlueprint blueprint)
    : MagicTailorBase(context.PageTokens.Expanded(new ModuleTokens(module)), parts: blueprint.Parts ?? new())
{
    public override string? Classes(string tag)
    {
        if (GetSettings(tag) is not { } styles) return null;
        var value = CombineWithModuleClasses(styles);
        return ProcessTokens(value);
    }


    /// <summary>
    /// Replace for container design rules
    /// </summary>
    /// <param name="styles"></param>
    /// <returns></returns>
    private string CombineWithModuleClasses(MagicBlueprintPart styles)
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