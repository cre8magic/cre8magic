using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Tokens;
using ToSic.Cre8magic.Utils.Logging;

namespace ToSic.Cre8magic.Pages.Internal;

internal abstract class MagicPagesFactoryBase(IContextWip context)
{
    #region Logging

    public IContextWip Context { get; } = context;

    internal Log Log => _log ??= context.LogRoot.GetLog(GetType().Name);
    private Log? _log;

    #endregion

    #region Abstract

    public abstract IMagicPageSetSettings Settings { get; }

    #endregion

    protected abstract IMagicPageDesigner FallbackDesigner();

    internal IMagicPageDesigner Design => _designer ??= context.PageDesigner ?? FallbackDesigner();
    private IMagicPageDesigner? _designer;

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
    public virtual List<IMagicPageWithDesignWip> GetChildren(IMagicPage page)
    {
        var l = Log.Fn<List<IMagicPageWithDesignWip>>($"{nameof(page.MenuLevel)}: {page.MenuLevel}");

        var pageFactory = context.PageFactory;
        var children = pageFactory.ChildrenOf(page.Id)
            .Select(child => new MagicPageWithDesign(context, pageFactory, this, child, page.MenuLevel + 1) as IMagicPageWithDesignWip)
            .ToList();
        return l.Return(children, $"{children.Count}");
    }


    #endregion
}