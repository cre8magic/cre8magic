using ToSic.Cre8magic.Settings.Values.Internal;
using ToSic.Cre8magic.Tailors;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Utils;
using ToSic.Cre8magic.Utils.Internal;

namespace ToSic.Cre8magic.Languages;

public class MagicLanguageTailor(CmThemeContextFull context, MagicLanguageSettings settings)
    : MagicTailorBase(context.PageTokens, settings.Blueprint?.Parts ?? new())
{
    /// <summary>
    /// TODO: PROBABLY MOVE TO Language?
    /// </summary>
    /// <param name="lang"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public string Classes(string tag, MagicLanguage? lang)
    {
        if (!tag.HasValue()) return "";
        if (GetSettings(tag) is not { } styles) return "";
        return styles.Classes + " " + styles.IsActive.Get(lang?.IsActive);
    }
}