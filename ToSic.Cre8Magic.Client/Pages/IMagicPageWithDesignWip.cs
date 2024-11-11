
// ReSharper disable once CheckNamespace
namespace ToSic.Cre8magic.Pages;

/// <summary>
/// An extended magic page which includes design information.
///
/// TODO: naming not final
/// </summary>
public interface IMagicPageWithDesignWip: IMagicPage, IMagicPageList
{

    ///// <inheritdoc cref="IMagicPageList.MenuLevel"/>
    new int MenuLevel { get; }

    ///// <inheritdoc cref="IMagicPageList.Classes"/>
    //new string? Classes(string tag);

    ///// <inheritdoc cref="IMagicPageList.Value"/>
    //new string? Value(string key);

}