
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

}