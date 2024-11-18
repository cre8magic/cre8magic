using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Utils;

namespace ToSic.Cre8magic.Languages;

internal class MagicLanguageDesigner(DesignerContextWip context) : MagicDesignerBase(context)
{
    internal string Classes(MagicLanguage? lang, string tag)
    {
        if (!tag.HasValue()) return "";
        if (GetSettings(tag) is not { } styles) return "";
        return styles.Classes + " " + styles.IsActive.Get(lang?.IsActive);
    }
}