using ToSic.Cre8magic.Designers;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Themes.Internal;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Languages;

public class MagicLanguageDesigner(MagicThemeContextFull context, MagicLanguageSettings settingsFull) : MagicDesignerBase(context, settingsFull.DesignSettings?.Parts)
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