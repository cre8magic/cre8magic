using ToSic.Cre8magic.Client.Models;

namespace ToSic.Cre8magic.Client.Pages;

public interface IPageDesigner
{
    string Classes(string tag, MagicPage page);
    string Value(string key, MagicPage page);
}