using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Pages;

public interface IPageDesigner
{
    string Classes(string tag, IMagicPage page);
    string Value(string key, IMagicPage page);
}