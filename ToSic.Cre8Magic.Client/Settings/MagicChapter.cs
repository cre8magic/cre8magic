using ToSic.Cre8magic.Analytics;
using ToSic.Cre8magic.Breadcrumbs;
using ToSic.Cre8magic.Containers;
using ToSic.Cre8magic.Menus;
using ToSic.Cre8magic.PageContexts;
using ToSic.Cre8magic.Themes.Settings;

namespace ToSic.Cre8magic.Settings;

public record MagicChapter
{
    public MagicChapter() { }

    public MagicChapter(string name)
    {
        Name = name;
    }

    public string? Name { get; init; }

    public MagicThemeSettings? Theme { get; init; }

    public MagicThemeBlueprint? ThemeBlueprint { get; init; }

    public MagicAnalyticsSettings? Analytics { get; init; }

    public MagicAnalyticsSettings? AnalyticsBlueprint { get; init; }

    public MagicBreadcrumbSettings? Breadcrumb { get; init; }

    public MagicBreadcrumbBlueprint? BreadcrumbBlueprint { get; init; }

    public MagicContainerSettings? Container { get; init; }

    public MagicContainerBlueprint? ContainerBlueprint { get; init; }

    public MagicLanguageSettings? Language { get; init; }

    public MagicLanguageBlueprint? LanguageBlueprint { get; init; }

    public MagicMenuSettings? Menu { get; init; }

    public MagicMenuBlueprint? MenuBlueprint { get; init; }

    public MagicPageContextSettings? PageContext { get; init; }
}