using System.Runtime.CompilerServices;
using Oqtane.Models;
using Oqtane.UI;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Settings;
using ToSic.Cre8magic.Themes.Internal;

namespace ToSic.Cre8magic;

internal class MagicFactoryWip(IMagicSettingsService settingsSvc) : IMagicFactoryWip
{
    public MagicContainerDesigner ContainerDesigner(PageState pageState, Module module)
    {
        if (_containerDesigners.TryGetValue(pageState.Page.PageId, out var designer))
            return designer;
        var allSettings = settingsSvc.GetSettings(pageState);

        var context = new DesignerContextWip(allSettings, pageState);
        var container = new MagicContainerDesigner(context, allSettings, module);
        _containerDesigners[module.ModuleId] = container;
        return container;
    }
    private readonly Dictionary<int, MagicContainerDesigner> _containerDesigners = new();

    public MagicThemeDesigner ThemeDesigner(PageState pageState)
    {
        if (_themeDesigners.TryGetValue(pageState.Page.PageId, out var designer))
            return designer;
        var allSettings = settingsSvc.GetSettings(pageState);

        var context = new DesignerContextWip(allSettings, pageState);
        var theme = new MagicThemeDesigner(context, allSettings);
        _themeDesigners[pageState.Page.PageId] = theme;
        return theme;
    }
    private readonly Dictionary<int, MagicThemeDesigner> _themeDesigners = new();

    public MagicLanguageDesigner LanguagesDesigner(PageState pageState)
    {
        if (_languagesDesigners.TryGetValue(pageState.Page.PageId, out var designer))
            return designer;
        var allSettings = settingsSvc.GetSettings(pageState);
        var context = new DesignerContextWip(allSettings, pageState);

        var languages = new MagicLanguageDesigner(context, allSettings);
        _languagesDesigners[pageState.Page.PageId] = languages;
        return languages;
    }
    private readonly Dictionary<int, MagicLanguageDesigner> _languagesDesigners = new();
}