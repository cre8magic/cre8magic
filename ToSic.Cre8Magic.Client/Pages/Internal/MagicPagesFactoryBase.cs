using System.Diagnostics.CodeAnalysis;
using ToSic.Cre8magic.Pages.Internal.PageDesign;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Pages.Internal;

internal abstract class MagicPagesFactoryBase(IContextWip context) : IMagicPageChildrenFactory
{
    #region Logging

    [field: AllowNull, MaybeNull]
    internal Log Log => field ??= context.LogRoot.GetLog(GetType().Name);

    #endregion

    #region Abstract

    public abstract IMagicPageSetSettings Settings { get; }

    #endregion

    protected abstract IMagicPageDesigner FallbackDesigner();

    [field: AllowNull, MaybeNull]
    internal IMagicPageDesigner Design => field ??= context.PageDesigner ?? FallbackDesigner();

    public IPageDesignHelperWip DesignHelper(IMagicPage page) => new PageDesignHelperWip(this, page);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="page">The page which would be used if any page property is requested</param>
    /// <returns></returns>
    internal TokenEngine PageTokenEngine(IMagicPage page)
    {
        var tokens = context.TokenEngineWip;
        // fallback without MagicSettings return just TokenEngine with PageTokens
        if (tokens == null)
            return new TokenEngine([new PageTokens(page)]);

        var originalPageTokens = (PageTokens)tokens.Parsers.First(p => p.NameId == PageTokens.NameIdConstant);
        var updatedPageTokens = originalPageTokens.Clone(page);
        return tokens.SwapParser(updatedPageTokens);
    }


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

        var pageFactory = context.PageFactory;
        var children = pageFactory.ChildrenOf(page.Id)
            .Select(IMagicPage (child) => new MagicPage(child.OqtanePage, pageFactory, this)
            {
                MenuLevel = page.MenuLevel + 1,
            })
            .ToList();
        return l.Return(children, $"{children.Count}");
    }


    #endregion
}