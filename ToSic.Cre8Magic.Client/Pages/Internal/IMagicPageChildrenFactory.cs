using ToSic.Cre8magic.Pages.Internal.PageDesign;

namespace ToSic.Cre8magic.Pages.Internal;

internal interface IMagicPageChildrenFactory
{
    /// <summary>
    /// Retrieve the children the first time it's needed.
    ///
    /// It's virtual, since other SetHelpers will have different implementations.
    /// For example the MagicMenuPageSetHelper will stop if a certain depth has been reached.
    /// </summary>
    /// <returns></returns>
    List<IMagicPage> ChildrenOf(IMagicPage page);

    IPageDesignHelperWip DesignHelper(IMagicPage page);
}