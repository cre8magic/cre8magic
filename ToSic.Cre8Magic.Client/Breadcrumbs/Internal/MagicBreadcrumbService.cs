using Oqtane.UI;
using ToSic.Cre8magic.Internal.Journal;
using ToSic.Cre8magic.Pages.Internal;
using ToSic.Cre8magic.Spells.Internal;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Breadcrumbs.Internal;

internal class MagicBreadcrumbService(IMagicSpellsService spellsSvc) : IMagicBreadcrumbService
{
    public IMagicBreadcrumbKit BreadcrumbKit(PageState pageState, MagicBreadcrumbSpell? settings = null)
    {
        var (settingsFull, themePart, _) = MergeSettings(pageState, settings);

        var pageFactory = new MagicPageFactory(pageState);
        var show = themePart?.Show != false;
        var (pages, childrenFactory) = pageFactory.Breadcrumb.Get(settingsFull);

        var root = new MagicPage(new() /* fake page, just for providing classes / values to the root outside the menu */, pageFactory, childrenFactory)
        {
            IsVirtualRoot = true,
            MenuLevel = 0,
        };

        return new MagicBreadcrumbKit
        {
            Pages = pages,
            Spell = settingsFull,
            Show = show,
            Root = root,
        };
    }

    private Data2WithJournal<MagicBreadcrumbSpell, MagicThemePartSettings?> MergeSettings(
        PageState pageState, MagicBreadcrumbSpell? settings)
    {
        var getSpell = new GetSpell(spellsSvc, pageState, settings?.Name);
        var spell = getSpell.GetBestSpell(settings, spellsSvc.Breadcrumbs);
        var bluePrint = getSpell.GetBestSpell(settings?.Blueprint, spellsSvc.BreadcrumbBlueprints, ThemePartSectionEnum.Design);
        return new(spell.Data with { Blueprint = bluePrint.Data }, getSpell.Part, spell.Journal.With(bluePrint.Journal));
    }
}