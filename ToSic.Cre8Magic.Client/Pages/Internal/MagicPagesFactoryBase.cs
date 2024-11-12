using ToSic.Cre8magic.Pages;

namespace ToSic.Cre8magic.Client.Pages.Internal;

internal abstract class MagicPagesFactoryBase(MagicPageFactory pageFactory)
{
    internal MagicPageFactory PageFactory => pageFactory;
    #region Logging

    internal LogRoot LogRoot { get; } = pageFactory.LogRoot;

    internal Log Log => _log ??= LogRoot.GetLog(GetType().Name);
    private Log? _log;

    #endregion

    #region Abstract

    public abstract IMagicPageSetSettings Settings { get; }

    #endregion

    protected abstract IMagicPageDesigner FallbackDesigner();
    public void Set(IMagicPageDesigner? designer) => _designer = designer;

    internal IMagicPageDesigner Design => _designer ??= FallbackDesigner();
    private IMagicPageDesigner? _designer;


    public void Set(MagicSettings? magicSettings) => MagicSettings = magicSettings;

    internal MagicSettings? MagicSettings { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="page">The page which would be used if any page property is requested</param>
    /// <returns></returns>
    internal TokenEngine PageTokenEngine(IMagicPage page)
    {
        // fallback without MagicSettings return just TokenEngine with PageTokens
        if (MagicSettings == null)
            return new TokenEngine([new PageTokens(page)]);

        var originalPage = (PageTokens)MagicSettings.Tokens.Parsers.First(p => p.NameId == PageTokens.NameIdConstant);
        originalPage = originalPage.Clone(page);
        return MagicSettings.Tokens.SwapParser(originalPage);
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

        var children = pageFactory.ChildrenOf(page.Id)
            .Select(child => new MagicPageWithDesign(pageFactory, this, child, page.MenuLevel + 1) as IMagicPageWithDesignWip)
            .ToList();
        return l.Return(children, $"{children.Count}");
    }


    #endregion
}