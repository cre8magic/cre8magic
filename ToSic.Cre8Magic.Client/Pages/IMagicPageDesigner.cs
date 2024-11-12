
namespace ToSic.Cre8magic.Pages;

/// <summary>
/// Interface for a designer of a magic page.
///
/// Anything implementing this interface can be used as a designer for a magic page,
/// providing custom classes and values when generating the HTML for the page.
/// </summary>
public interface IMagicPageDesigner
{
    string Classes(string tag, IMagicPage page);
    string Value(string key, IMagicPage page);
}