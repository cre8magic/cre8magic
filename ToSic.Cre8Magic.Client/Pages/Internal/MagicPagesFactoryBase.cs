using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Pages.Internal.PageDesign;
using ToSic.Cre8magic.Settings.Internal;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Pages.Internal;

internal abstract class MagicPagesFactoryBase(WorkContext workContext) : IMagicPageChildrenFactory
{
    #region Logging

    [field: AllowNull, MaybeNull]
    internal Log Log => field ??= workContext.LogRoot.GetLog(GetType().Name);

    internal TokenEngine TokenEngine => workContext.TokenEngine;

    #endregion

    protected abstract IMagicPageTailor PageTailor();

    public IPageDesignHelperWip PageDesignHelper(IMagicPage page) => new PageDesignHelperWip(this, page, PageTailor());


    #region Children

    /// <summary>
    /// Retrieve the children the first time it's needed.
    ///
    /// It's virtual, since other SetHelpers will have different implementations.
    /// For example the MagicMenuPageSetHelper will stop if a certain depth has been reached.
    /// </summary>
    /// <returns></returns>
    public virtual List<IMagicPage> ChildrenOf(IMagicPage page)
    {
        var l = Log.Fn<List<IMagicPage>>($"{nameof(page.MenuLevel)}: {page.MenuLevel}");

        var pageFactory = workContext.PageFactory;
        var children = pageFactory.ChildrenOf(page.Id)
            .Select(IMagicPage (child) => new MagicPage(child.RawPage, pageFactory, this)
            {
                MenuLevel = page.MenuLevel + 1,
            })
            .ToList();
        return l.Return(children, $"{children.Count}");
    }


    #endregion
}