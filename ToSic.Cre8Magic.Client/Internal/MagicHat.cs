using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Breadcrumbs.Internal;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Themes.Internal;

namespace ToSic.Cre8magic.Internal;

internal class MagicHat(
    IMagicSettingsService settingsSvc,
    MagicLazy<IMagicBreadcrumbService> breadcrumbSvc
    ) : IMagicHat
{
    /// <inheritdoc />
    public IMagicBreadcrumbKit BreadcrumbKit(PageState pageState, MagicBreadcrumbSettings? settings = default) =>
        breadcrumbSvc.Value.BreadcrumbKit(pageState, settings);


    public MagicContainerDesigner ContainerDesigner(PageState pageState, Module module)
    {
        if (_containerDesigners.TryGetValue(pageState.Page.PageId, out var designer))
            return designer;

        var designContext = settingsSvc.GetThemeContextFull(pageState);
        var container = new MagicContainerDesigner(designContext, module);
        _containerDesigners[module.ModuleId] = container;
        return container;
    }
    private readonly Dictionary<int, MagicContainerDesigner> _containerDesigners = new();

    public MagicThemeDesigner ThemeDesigner(PageState pageState)
    {
        if (_themeDesigners.TryGetValue(pageState.Page.PageId, out var designer))
            return designer;

        var designContext = settingsSvc.GetThemeContextFull(pageState);
        var theme = new MagicThemeDesigner(designContext);
        _themeDesigners[pageState.Page.PageId] = theme;
        return theme;
    }
    private readonly Dictionary<int, MagicThemeDesigner> _themeDesigners = new();

    public MagicLanguageDesigner LanguageDesigner(PageState pageState)
    {
        if (_languagesDesigners.TryGetValue(pageState.Page.PageId, out var designer))
            return designer;
        var designContext = settingsSvc.GetThemeContextFull(pageState);

        var languages = new MagicLanguageDesigner(designContext);
        _languagesDesigners[pageState.Page.PageId] = languages;
        return languages;
    }
    private readonly Dictionary<int, MagicLanguageDesigner> _languagesDesigners = new();
}