using ToSic.Cre8magic.Client.Breadcrumbs.Settings;
using ToSic.Cre8magic.Client.Models;

namespace ToSic.Cre8magic.Client.Breadcrumbs;

internal class MagicBreadcrumbHelper(MagicPageFactory pageFactory)
{
    public void Set(MagicBreadcrumbSettings settings) => _settings = settings;

    public MagicBreadcrumbSettings Settings => _settings ??= MagicBreadcrumbSettings.Defaults.Fallback;
    private MagicBreadcrumbSettings? _settings;

    public void Set(IBreadcrumbDesigner designer) => _designer = designer;

    internal IBreadcrumbDesigner Design => _designer ??= new MagicBreadcrumbDesigner(Settings);
    private IBreadcrumbDesigner? _designer;

    public void Set(MagicSettings magicSettings) => MagicSettings = magicSettings;

    internal MagicSettings? MagicSettings { get; private set; }

    internal TokenEngine PageTokenEngine(MagicPage page)
    {
        // fallback without MagicSettings return just TokenEngine with PageTokens
        if (MagicSettings == null)
            return new TokenEngine([new PageTokens(pageFactory, page)]);

        var originalPage = (PageTokens)MagicSettings.Tokens.Parsers.First(p => p.NameId == PageTokens.NameIdConstant);
        originalPage = originalPage.Clone(page);
        return MagicSettings.Tokens.SwapParser(originalPage);
    }

}