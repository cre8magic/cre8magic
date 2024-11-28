using System;
using System.Collections.Generic;
using ToSic.Module.Cre8MagicTests.Client.Analytics;
using ToSic.Module.Cre8MagicTests.Client.Breadcrumb.Tests;
using ToSic.Module.Cre8MagicTests.Client.Languages.Tests;
using ToSic.Module.Cre8MagicTests.Client.Menus.Tests;
using ToSic.Module.Cre8MagicTests.Client.PageService;
using ToSic.Module.Cre8MagicTests.Client.RandomTests;

namespace ToSic.Module.Cre8MagicTests.Client;

/// <summary>
/// 
/// </summary>
/// <param name="Id">The ID which is also stored in the DB, so this should remain constant even if the controls change</param>
/// <param name="Name"></param>
/// <param name="Type"></param>
internal record TestDescription(string Id, string Name, Type Type);

internal class TestDatabase
{
    public static List<TestDescription> Tests =
    [
        // Analytics
        new("analytics-inject-settings", "Analytics - Inject Settings", typeof(TestSettingsProvidersAnalytics)),
        new("analytics-inject-settings-read-only", "Analytics - Read Injected Settings or Default", typeof(TestReadingProvidersAnalytics)),

        // Breadcrumb Controls
        new("breadcrumbs-basic", "Breadcrumb Basic", typeof(BreadcrumbsBasic)),
        new("breadcrumb-demo-code", "Breadcrumb Demo Code", typeof(DemoBreadcrumbUsingCode)),

        // Language Controls
        new("language-basic", "Language - Basic (various configs)", typeof(TestLanguagesBasic)),
        new("language-custom-catalog", "Language - Custom Catalog", typeof(TestLanguagesWithCustomCatalog)),

        // Page Service
        new("page-service-pages", "Page Service Pages", typeof(PageServicePages)),
        new("page-service-breadcrumb", "Page Service Breadcrumb", typeof(PageServiceBreadcrumb)),

        // Menu Service
        new("menu-basic", "Menus Basic", typeof(MenuControlsBasic)),
        new("menu-functions", "Menus Functions", typeof(MenuControlsFunctions)),
        new("menu-provide-design-red", "Menus Provide Design Red", typeof(MenuProvideDesignRed)),
        new("menu-many", "Menu - Test Many", typeof(MenuTestMany)),

        // Special Tests
        new("sys-random-number-by-page", "Random Number by Page", typeof(TestGetKeep)),
    ];
}